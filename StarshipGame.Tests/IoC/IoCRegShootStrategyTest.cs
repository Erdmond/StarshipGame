namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyShootStrategyTests
{
    private readonly Mock<ICommand> authCommandMock;
    private readonly Mock<ICommand> createProjectileCommandMock;
    private List<ICommand> capturedMacroCommands;
    private ICommand capturedEnqueuedCommand;

    public RegisterIoCDependencyShootStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        authCommandMock = new Mock<ICommand>();
        createProjectileCommandMock = new Mock<ICommand>();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.Auth", (object[] args) => authCommandMock.Object).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.CreateProjectile", (object[] args) => createProjectileCommandMock.Object).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.Macro", (object[] args) =>
        {
            capturedMacroCommands = args.Select(x => (ICommand)x).ToList();
            return (new Mock<ICommand>()).Object;
        }).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Game.EnqueueCommand", (object[] args) =>
        {
            capturedEnqueuedCommand = args[0] as ICommand;
            return (new Mock<ICommand>()).Object;
        }).Execute();
    }

    [Fact]
    public void ShootStrategy_EnqueuesMacroCommand_WithAuthAndCreateProjectile()
    {
        var registerCommand = new RegisterIoCDependencyShootStrategy();
        registerCommand.Execute();

        var playerInfo = new Mock<IObjectInfo>().Object;
        var shooterInfo = new Mock<IObjectInfo>().Object;

        ICommand shootStrategy = IoC.Resolve<ICommand>("Commands.ShootStrategy", playerInfo, shooterInfo);
        shootStrategy.Execute();

        Assert.NotNull(capturedEnqueuedCommand);
        Assert.NotNull(capturedMacroCommands);
        Assert.Equal(2, capturedMacroCommands.Count);
        Assert.Same(authCommandMock.Object, capturedMacroCommands[0]);
        Assert.Same(createProjectileCommandMock.Object, capturedMacroCommands[1]);
    }
}
