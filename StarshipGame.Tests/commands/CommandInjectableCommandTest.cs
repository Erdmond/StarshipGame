using Moq;

namespace StarshipGame.Tests;

public class CommandInjectableCommandTest
{
    [Fact]
    public void InjectedCommandHasBeenExecuted()
    {
        Mock<Hwdtech.ICommand> innerCommand = new Mock<Hwdtech.ICommand>();
        innerCommand.Setup(e => e.Execute());
        CommandInjectableCommand cmd = new CommandInjectableCommand();

        cmd.Inject(innerCommand.Object);
        cmd.Execute();

        innerCommand.Verify(e => e.Execute(), Times.Once);
    }

    [Fact]
    public void NotSetInnerCommandThrows()
    {
        CommandInjectableCommand cmd = new CommandInjectableCommand();
        Assert.Throws<NullReferenceException>(() => cmd.Execute());
    }
}
