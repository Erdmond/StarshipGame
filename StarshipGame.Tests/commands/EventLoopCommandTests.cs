using Hwdtech.Ioc;
using Moq;
using Xunit;

namespace StarshipGame.Tests;

public class EventLoopCommandTests
{
    public EventLoopCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        var newScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", newScope).Execute();
    }

    [Fact]
    public void Execute_CallsQueueTakeCommand()
    {
        var mockCommand = new Mock<ICommand>();
        IoC.Resolve<ICommand>("IoC.Register", "Queue.Take", (object[] args) => mockCommand.Object).Execute();

        new EventLoopCommand().Execute();

        mockCommand.Verify(cmd => cmd.Execute(), Times.Once);
    }
}
