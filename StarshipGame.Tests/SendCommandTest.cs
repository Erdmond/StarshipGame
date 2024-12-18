namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class SendCommandTests
{
    public SendCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void SendCommandSendsCommandToIMessageReceiver()
    {
        var cmd = new Mock<Hwdtech.ICommand>();
        var receiver = new Mock<IMessageReceiver>();

        var sendCmd = new SendCommand(cmd.Object, receiver.Object);
        sendCmd.Execute();

        receiver.Verify(r => r.Receive(cmd.Object), Times.Once());
    }

    [Fact]
    public void IMessageReceiverCannotAcceptLongOperation()
    {
        var cmd = new Mock<ICommand>();
        var receiver = new Mock<IMessageReceiver>();

        receiver.Setup(r => r.Receive(It.IsAny<ICommand>())).Throws(new Exception("Receiver cannot accept long operation"));

        var sendCmd = new SendCommand(cmd.Object, receiver.Object);

        var exception = Assert.Throws<Exception>(() => sendCmd.Execute());

        Assert.Equal("Receiver cannot accept long operation", exception.Message);
    }
}
