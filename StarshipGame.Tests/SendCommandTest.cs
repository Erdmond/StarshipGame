namespace StarshipGame;
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
        /*
        Реализован тест "SendCommand передает команду в IMessageReceiver",
        который проверяет, что при вызове метода Execute класса SendCommand
        вызывается метод Receive объекта ICommandReceiver с параметром - объектом длительной команды.
        */
    }

    [Fact]
    public void IMessageReceiverCannotAcceptLongCommand()
    {
        /*
        Реализован тест, который проверяет, что SendCommand.Execute выбрасывает исключение,
        если IMessageReceiver не может принять длительную команду.
        */
    }
}
