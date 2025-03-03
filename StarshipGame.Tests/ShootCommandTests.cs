namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class ShootCommandTests
{
    private readonly Mock<ICommand> authCommandMock;
    private readonly Mock<ICommand> createTorpedoCommandMock;
    private ICommand capturedMacroCommand;

    public ShootCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        authCommandMock = new Mock<ICommand>();
        createTorpedoCommandMock = new Mock<ICommand>();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.Auth", (object[] args) => authCommandMock.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Commands.CreateTorpedo", (object[] args) => createTorpedoCommandMock.Object).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.GetICommandsFromArgs", (object[] args) =>
            args.Select(x => (ICommand)x).ToList()).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Commands.Macro", (object[] args) =>
            new MacroCommand(IoC.Resolve<List<ICommand>>("Commands.GetICommandsFromArgs", args))).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Game.EnqueueCommand", (object[] args) =>
        {
            capturedMacroCommand = args[0] as ICommand;
            var mock = new Mock<ICommand>();
            mock.Setup(cmd => cmd.Execute());
            return mock.Object;
        }).Execute();
    }

    [Fact]
    public void ShootCommand_SuccessfulShot_EnqueuesMacroCommand()
    {
        authCommandMock.Setup(cmd => cmd.Execute());
        createTorpedoCommandMock.Setup(cmd => cmd.Execute());

        var objectInfo = new Mock<IObjectInfo>().Object;
        var shootCommand = new ShootCommand(objectInfo);
        shootCommand.Execute();

        Assert.NotNull(capturedMacroCommand);
        capturedMacroCommand.Execute();
        authCommandMock.Verify(cmd => cmd.Execute(), Times.Once());
        createTorpedoCommandMock.Verify(cmd => cmd.Execute(), Times.Once());
    }

    [Fact]
    public void ShootCommand_AuthenticationFails_EnqueuedMacroCommandThrows()
    {
        authCommandMock.Setup(cmd => cmd.Execute()).Throws(new Exception("Authentication failed"));
        createTorpedoCommandMock.Setup(cmd => cmd.Execute());

        var objectInfo = new Mock<IObjectInfo>().Object;
        var shootCommand = new ShootCommand(objectInfo);
        shootCommand.Execute();

        Assert.NotNull(capturedMacroCommand);
        var ex = Assert.Throws<Exception>(() => capturedMacroCommand.Execute());
        Assert.Equal("Authentication failed", ex.Message);

        authCommandMock.Verify(cmd => cmd.Execute(), Times.Once());
        createTorpedoCommandMock.Verify(cmd => cmd.Execute(), Times.Never());
    }

    [Fact]
    public void ShootCommand_EnqueuedMacroCommand_ExecutesCommandsInOrder()
    {
        var executionOrder = new List<string>();
        authCommandMock.Setup(cmd => cmd.Execute()).Callback(() => executionOrder.Add("auth"));
        createTorpedoCommandMock.Setup(cmd => cmd.Execute()).Callback(() => executionOrder.Add("create"));

        var objectInfo = new Mock<IObjectInfo>().Object;
        var shootCommand = new ShootCommand(objectInfo);
        shootCommand.Execute();

        Assert.NotNull(capturedMacroCommand);
        capturedMacroCommand.Execute();

        Assert.Equal(new[] { "auth", "create" }, executionOrder);
    }
}
