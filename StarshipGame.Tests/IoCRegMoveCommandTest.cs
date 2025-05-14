using Moq;
using Xunit;
using Hwdtech;
using Hwdtech.Ioc;

namespace StarshipGame.Tests;

public class RegisterIoCDependencyMoveCommandTests
{
    public RegisterIoCDependencyMoveCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.IMovingObject", (object[] args) => (new Mock<IMovable>().Object)).Execute();
    }

    [Fact]
    public void Execute_RegistersMoveCommandDependency()
    {
        var registerCommand = new RegisterIoCDependencyMoveCommand();
        registerCommand.Execute();

        var gameObj = new Mock<IMovable>().Object;

        var moveCommand = IoC.Resolve<ICommand>("Commands.Move", gameObj);

        Assert.NotNull(moveCommand);
        Assert.IsType<MoveCommand>(moveCommand);
    }
}
