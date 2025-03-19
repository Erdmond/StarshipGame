using System.Reflection;
using Hwdtech.Ioc;
using Moq;
using Xunit;

namespace StarshipGame.Tests;

public class GameCommandTest
{
    public GameCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var rootScope = IoC.Resolve<object>("Scopes.Root");
        var gameScope = IoC.Resolve<object>("Scopes.New", rootScope);
        IoC.Resolve<ICommand>("Scopes.Current.Set", gameScope).Execute();
        new IoCRegGameInitializationCommand().Execute();
    }

    //выполнение как минимум одной итерации игрового цикла
    [Fact]
    public void Game_Initialization_Success()
    {
        int cycleCount = 0;
        var commandQueue = new Queue<ICommand>();

        var eventLoopMock = new Mock<ICommand>();
        eventLoopMock.Setup(cmd => cmd.Execute());

        IoC.Resolve<ICommand>("Game.Initialize", () => cycleCount++ < 3, eventLoopMock.Object,
            () => commandQueue.Dequeue(),
            (ICommand cmd) => new RunnableCommand(() => commandQueue.Enqueue(cmd))
        ).Execute();

        eventLoopMock.Verify(cmd => cmd.Execute(), Times.AtLeastOnce());
    }
}
