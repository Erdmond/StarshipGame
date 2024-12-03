namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;


public class CmdEndCommandTest
{
    Mock<IInjectable> i = new Mock<IInjectable>();
    Mock<Hwdtech.ICommand> endableCmd = new Mock<Hwdtech.ICommand>();

    public CmdEndCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "InitialValues.Delete", (object[] args) => (new Mock<Hwdtech.ICommand>()).Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Object", (object[] args) => i.Object).Execute();
    }
    [Fact]
    public void CmdEndCommandTestPostItCommandInQueue()
    {
        var order = new Mock<IOrder>();
        order.Setup(s => s.Target).Returns((new Mock<IObject>()).Object);

        var cmdEndCommand = new CmdEndCommand(endableCmd.Object, order.Object);

        cmdEndCommand.Execute();

        i.Verify(i => i.Inject(It.IsAny<EmptyCommand>()), Times.Once());
    }
    [Fact]
    public void ImpossibleToDetermineTarget()
    {
        var order = new Mock<IOrder>();
        order.Setup(s => s.Target).Throws(() => new Exception()).Verifiable();

        var cmdEndCommand = new CmdEndCommand(endableCmd.Object, order.Object);

        Assert.Throws<Exception>(cmdEndCommand.Execute);
    }
}