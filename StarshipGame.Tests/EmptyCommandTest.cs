namespace StarshipGame.Tests;

public class EmptyCommandTest
{
    [Fact]
    public void WorkProperly()
    {
        EmptyCommand cmd = new EmptyCommand();
        cmd.Execute();
    }
}
