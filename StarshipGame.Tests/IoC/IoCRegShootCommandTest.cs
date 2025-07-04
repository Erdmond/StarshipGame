namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyShootCommandTests
{
    public RegisterIoCDependencyShootCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegistersShootCommandDependency()
    {
        var registerCommand = new RegisterIoCDependencyShootCommand();
        registerCommand.Execute();

        var playerInfo = new Mock<IObjectInfo>().Object;
        var shooterInfo = new Mock<IObjectInfo>().Object;

        var shootCommand = IoC.Resolve<ICommand>("Commands.Shoot", playerInfo, shooterInfo);

        Assert.NotNull(shootCommand);
        Assert.IsType<ShootCommand>(shootCommand);
    }
}
