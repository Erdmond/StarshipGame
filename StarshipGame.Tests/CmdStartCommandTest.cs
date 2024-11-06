using Moq;

namespace StarshipGame.Tests;

public class CmdStartCommandTest
{
    [Fact]
    public void StartTest()
    {
        Mock<ICommand> mockCmd = new Mock<ICommand>();
        Mock<IStartable> mockOrder = new Mock<IStartable>();

        CmdStartCommand startCmd = new CmdStartCommand(mockCmd, mockOrder);

        startCmd.Execute();

        mockOrder.Verify(a => a.Recieve(It.IsAny<ICommand>()), Times.Once);
    }
}
