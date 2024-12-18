namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencySendCommandTests
{
    public RegisterIoCDependencySendCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegistersSendCommandDependency()
    {
        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();

        var cmd = new Mock<Hwdtech.ICommand>();
        var receiver = new Mock<IMessageReceiver>();
        var sendCommand = IoC.Resolve<Hwdtech.ICommand>("Commands.Send", cmd.Object, receiver.Object);

        Assert.NotNull(sendCommand);
        Assert.IsType<SendCommand>(sendCommand);
    }
}
