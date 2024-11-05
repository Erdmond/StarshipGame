using Moq;
using StarshipGame;

namespace StarshipGame.Test;

public class RepeatCommandTests
{
    [Fact]
    public void CommandInReciever()
    {
        Mock<ICommand> mockCommand = new Mock<ICommand>();
        Mock<ICommandReciever> mockReciever = new Mock<ICommandReciever>(); 

        RepeatCommand repeatCommand = new RepeatCommand(mockCommand.Object, mockReciever.Object);

        repeatCommand.Execute();

        mockReciever.Verify(q => q.Enqueue(It.IsAny<ICommand>()), Times.Once); 
    }
}