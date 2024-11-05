using Moq;

namespace StarshipGame.Test;

public class BridgeCommandTest
{
    /***
     * tests:
     * 1) good work
     * 2) command is empty and exception
     */

    [Fact]
    public void BridgeWorking()
    {
        Mock<ICommand> innerCommand = new Mock<ICommand>();
        innerCommand.Setup(e => e.Execute());
        BridgeCommand bridge = new BridgeCommand();
        bridge.Inject(innerCommand.Object);

        bridge.Execute();

        innerCommand.Verify(mock => mock.Execute(), Times.Once());
    }

    [Fact]
    public void BridgeCalledWithNoCommand()
    {
        BridgeCommand bridge = new BridgeCommand();
        Assert.Throws<NullReferenceException>(() => bridge.Execute());
    }
}