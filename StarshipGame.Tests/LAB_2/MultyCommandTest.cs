using Moq;
using StarshipGame;

namespace StarshipGame.Test;

public class MultyCommandTests
{
    [Fact]
    public void Execute_ShouldExecuteAllCommandsInQueue()
    {
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();
        var mockCommand3 = new Mock<ICommand>();

        var commands = new Queue<ICommand>(new[] {
            mockCommand1.Object,
            mockCommand2.Object,
            mockCommand3.Object
        });

        var multyCommand = new MultyCommand(commands);

        multyCommand.Execute();

        mockCommand1.Verify(c => c.Execute(), Times.Once);
        mockCommand2.Verify(c => c.Execute(), Times.Once);
        mockCommand3.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_ShouldEmptyTheQueue()
    {
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();
        var mockCommand3 = new Mock<ICommand>();

        var commands = new Queue<ICommand>(new[] {
            mockCommand1.Object,
            mockCommand2.Object,
            mockCommand3.Object
        });

        var multyCommand = new MultyCommand(commands);

        multyCommand.Execute();

        Assert.Empty(commands);
    }
}
