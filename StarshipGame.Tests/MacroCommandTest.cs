using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using System.Linq;

namespace StarshipGame.Tests;


public class MacroCommandTest
{
    public MacroCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.GetICommandsFromArgs",
            (object[] args) => args.ToList().Select(c => (Hwdtech.ICommand)c).ToArray()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Macro",
            (object[] args) => new MacroCommand(IoC.Resolve<Hwdtech.ICommand[]>("Commands.GetICommandsFromArgs", args))).Execute();
    }

    [Fact]
    public void MacroExecutesAllCommandsSuccessfully()
    {
        Mock<Hwdtech.ICommand> cmd1 = new Mock<Hwdtech.ICommand>();
        Mock<Hwdtech.ICommand> cmd2 = new Mock<Hwdtech.ICommand>();
        cmd1.Setup(c => c.Execute());
        cmd2.Setup(c => c.Execute());

        IoC.Resolve<Hwdtech.ICommand>("Commands.Macro", cmd1.Object, cmd2.Object).Execute();

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void MacroCommandExecuteThrowsExceptionIfCommandThrowsException()
    {
        Mock<Hwdtech.ICommand> cmd1 = new Mock<Hwdtech.ICommand>();
        Mock<Hwdtech.ICommand> cmdException = new Mock<Hwdtech.ICommand>();
        Mock<Hwdtech.ICommand> cmd2 = new Mock<Hwdtech.ICommand>();
        cmd1.Setup(c => c.Execute());
        cmdException.Setup(c => c.Execute()).Throws<Exception>();
        cmd2.Setup(c => c.Execute());

        Assert.Throws<Exception>(() =>
            IoC.Resolve<Hwdtech.ICommand>("Commands.Macro", cmd1.Object, cmdException.Object, cmd2.Object).Execute());

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmdException.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Never);
    }
}
