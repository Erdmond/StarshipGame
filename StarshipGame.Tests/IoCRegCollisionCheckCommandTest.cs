namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyCollisionCheckCommandTests
{
    public RegisterIoCDependencyCollisionCheckCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var exception = new Exception();
        var criteria = true;

        IoC.Resolve<ICommand>("IoC.Register", "Collision.Strategy", (object[] args) => (object)criteria).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Exceptions.Collision", (object[] args) => exception).Execute();
    }

    [Fact]
    public void Execute_RegistersCollisionCheckCommandDependency()
    {
        var registerCommand = new RegisterIoCDependencyCollisionCheckCommand();
        registerCommand.Execute();

        var obj1 = new Mock<ICollisionObject>();
        var obj2 = new Mock<ICollisionObject>();
        var checkCollisionCommand = IoC.Resolve<Hwdtech.ICommand>("Commands.CollisionCheck", obj1.Object, obj2.Object);

        Assert.NotNull(checkCollisionCommand);
        Assert.IsType<CollisionCheckCommand>(checkCollisionCommand);
    }
}
