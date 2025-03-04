namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class ShootCommandTests
{
    private readonly Mock<ICommand> _shootStrategyCommand;

    public ShootCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        _shootStrategyCommand = new Mock<ICommand>();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.ShootStrategy", (object[] args) => _shootStrategyCommand.Object).Execute();
    }

    [Fact]
    public void ShootCommand_ShouldExecute_ShootStrategyCommand()
    {
        var playerInfo = new Mock<IObjectInfo>();
        var shooterInfo = new Mock<IObjectInfo>();

        var shootCommand = new ShootCommand(playerInfo.Object, shooterInfo.Object);

        shootCommand.Execute();

        _shootStrategyCommand.Verify(cmd => cmd.Execute(), Times.Once);
    }
}
