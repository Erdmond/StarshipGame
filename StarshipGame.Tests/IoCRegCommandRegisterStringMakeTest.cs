using Hwdtech.Ioc;
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
    public void IocStrategyWithAdapterMake()
    {
        var answer = IoC.Resolve<string>("Adapters.Command.MakeLine", "MoveCommand", "Movable");

        Assert.Equal(
            "IoC.Resolve<ICommand>(\"IoC.Register\", \"Commands.MoveCommand\", (object[] args) => new MoveCommand(IoC.Resolve<IMovable>(\"Adapters.Movable\", args[0]))).Execute();",
            answer);
    }

    [Fact]
    public void WrongCommandName()
    {
        Assert.Throws<Exception>(() => IoC.Resolve<string>("Adapters.Command.MakeLine", "", "TestClassName"));
    }
    
    [Fact]
    public void WrongClassName()
    {
        Assert.Throws<Exception>(() => IoC.Resolve<string>("Adapters.Command.MakeLine", "TestCommandName", ""));
    }
}