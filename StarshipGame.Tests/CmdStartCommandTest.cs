namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class CmdStartCommandTest
{
    Mock<IQueue> q = new Mock<IQueue>();
    Mock<Hwdtech.ICommand> startableCmd = new Mock<Hwdtech.ICommand>();

    public CmdStartCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "InitialValues.Set", (object[] args) => (new Mock<Hwdtech.ICommand>()).Object).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GQueue", (object[] args) => q.Object).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command.Set", (object[] args) => (new Mock<Hwdtech.ICommand>()).Object).Execute();
    }

    [Fact]
    public void CmdStartCommandTestRegistersInitialValuesAndPutsCommandInQueue()
    {
        var order = new Mock<IOrder>();
        order.Setup(s => s.Target).Returns((new Mock<IObject>()).Object);
        order.Setup(s => s.InitialValues).Returns(new Dictionary<string, object>());

        var cmdStartCommand = new CmdStartCommand(order.Object, startableCmd.Object);

        cmdStartCommand.Execute();

        order.Verify(s => s.InitialValues, Times.Once());
        q.Verify(q => q.Put(It.IsAny<Hwdtech.ICommand>()), Times.Once());
    }

    [Fact]
    public void ImpossibleToDetermineTarget()
    {
        var order = new Mock<IOrder>();
        order.Setup(s => s.Target).Throws(() => new Exception()).Verifiable();
        order.Setup(s => s.InitialValues).Returns(new Dictionary<string, object>());

        var cmdStartCommand = new CmdStartCommand(order.Object, startableCmd.Object);

        Assert.Throws<Exception>(cmdStartCommand.Execute);
    }

    [Fact]
    public void ImpossibleToDetermineInitialValues()
    {
        var order = new Mock<IOrder>();
        order.Setup(s => s.Target).Returns((new Mock<IObject>()).Object);
        order.Setup(s => s.InitialValues).Throws(() => new Exception()).Verifiable();

        var cmdStartCommand = new CmdStartCommand(order.Object, startableCmd.Object);

        Assert.Throws<Exception>(cmdStartCommand.Execute);
    }
}
