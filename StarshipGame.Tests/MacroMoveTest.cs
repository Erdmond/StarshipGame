using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests;

public class MacroMoveTest
{
    public MacroMoveTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        new RegisterIoCDependencyMacroCommand().Execute();
    }

    [Fact]
    public void MacroExecutesAndReturnsCommand()
    {
        Mock<Hwdtech.ICommand> cmd = new Mock<Hwdtech.ICommand>();
        cmd.Setup(c => c.Execute());
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Specs.Move", (object[] args) => new string[] { "A" }).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "A", (object[] args) => cmd.Object).Execute();

        new RegisterIoCDependencyMacroMoveRotate().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Macro.Move").Execute();

        cmd.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void SpecsNotSpecifiedAndThrowsException()
    {
        new RegisterIoCDependencyMacroMoveRotate().Execute();

        Assert.Throws<ArgumentException>(() => IoC.Resolve<Hwdtech.ICommand>("Macro.Move").Execute());
    }
}
