using Moq;
namespace StarshipGame.Tests;


public class BridgeCommandTest
{
    [Fact]
    public void WorkProperly()
    {
        Mock<ICommand> innerCommand = new Mock<ICommand>();
        innerCommand.Setup(e => e.Execute());
        BridgeCommand cmd = new BridgeCommand();
        
        cmd.Inject(innerCommand.Object);
        cmd.Execute();
        
        innerCommand.Verify(e => e.Execute(), Times.Once);
    }

    [Fact]
    public void NotSetInnerCommandThrows()
    {
        BridgeCommand cmd = new BridgeCommand();
        Assert.Throws<NullReferenceException>(() => cmd.Execute());
    }
}
