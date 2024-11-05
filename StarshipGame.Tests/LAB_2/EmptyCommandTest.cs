namespace StarshipGame.Test;

public class EmptyCommandTest
{
    [Fact]
    public void TestEmptyCommand()
    {
        EmptyCommand cmd = new EmptyCommand();
        cmd.Execute();
    }
}