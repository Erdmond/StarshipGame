using Hwdtech.Ioc;
using Xunit;
using Xunit.Abstractions;

namespace StarshipGame.Tests;

public class IoCRegCommandRegisterStringMakeTest
{
    public IoCRegCommandRegisterStringMakeTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        new IoCRegCommandRegisterStringMake().Execute();
    }

    [Fact]
    public void IocStrategyWithAdapterMake_GeneratesValidFactoryClass()
    {
        var generatedCode = IoC.Resolve<string>("Adapters.Command.MakeLine", "MoveCommand", "Movable");

        Assert.Contains("public class MoveCommandFactory", generatedCode);
        Assert.Contains("public void Execute()", generatedCode);
        Assert.Contains("IoC.Resolve<ICommand>(\"IoC.Register\", \"Commands.MoveCommand\"", generatedCode);
        Assert.Contains("new MoveCommand(", generatedCode);
        Assert.Contains("IoC.Resolve<IMovable>(\"Adapters.Movable\", args[0])", generatedCode);
    }

    [Fact]
    public void WrongCommandName_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            IoC.Resolve<string>("Adapters.Command.MakeLine", "", "TestClassName"));
    }

    [Fact]
    public void WrongClassName_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            IoC.Resolve<string>("Adapters.Command.MakeLine", "TestCommandName", ""));
    }
}
