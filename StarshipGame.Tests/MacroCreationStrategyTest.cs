using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests;

public class MacroCreationStrategyTest
{
    private readonly object[] _testArguments = new object[] { };

    public MacroCreationStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        new RegisterIoCDependencyMacroCommand().Execute();
    }

    [Fact]
    public void TestSpecsMacroCommandBuildsAndExecutes()
    {
        Mock<Hwdtech.ICommand> cmd1 = new Mock<Hwdtech.ICommand>();
        Mock<Hwdtech.ICommand> cmd2 = new Mock<Hwdtech.ICommand>();
        Mock<Hwdtech.ICommand> cmd3 = new Mock<Hwdtech.ICommand>();
        cmd1.Setup(c => c.Execute());
        cmd2.Setup(c => c.Execute());
        cmd3.Setup(c => c.Execute());
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Specs.Test", (object[] args) => new string[] { "A", "B" })
            .Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "A", (object[] args) => cmd1.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "B", (object[] args) => cmd2.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "C", (object[] args) => cmd3.Object).Execute();


        Hwdtech.ICommand macro = new CreateMacroCommandStrategy("Specs.Test").Resolve(_testArguments);
        macro.Execute();

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
        cmd3.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void SpecsAreNotResolved()
    {
        Assert.Throws<ArgumentException>(() => new CreateMacroCommandStrategy("Specs.Test").Resolve(_testArguments));
    }

    [Fact]
    public void OneOfCommandsIsNotResolved()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Specs.Test", (object[] args) => new string[] { "A", "B" })
            .Execute();
        Assert.Throws<ArgumentException>(() => new CreateMacroCommandStrategy("Specs.Test").Resolve(_testArguments));
    }
}
