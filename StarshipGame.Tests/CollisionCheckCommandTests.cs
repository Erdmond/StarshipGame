namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class CollisionCheckCommandTests
{
    public CollisionCheckCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var exception = new Exception();
        IoC.Resolve<ICommand>("IoC.Register", "Exceptions.Collision", (object[] args) => exception).Execute();
    }

    [Fact]
    public void Command_Successfull_Create()
    {
        var obj1 = new Mock<ICollisionObject>();
        var obj2 = new Mock<ICollisionObject>();
        IoC.Resolve<ICommand>("IoC.Register", "Collision.Strategy", (object[] args) => (object)true).Execute();

        var command = new CollisionCheckCommand(obj1.Object, obj2.Object);

        Assert.NotNull(command);
    }

    [Fact]
    public void Command_Unsuccessfull_Create()
    {
        Assert.Throws<ArgumentNullException>(() => new CollisionCheckCommand(null, new Mock<ICollisionObject>().Object));
        Assert.Throws<ArgumentNullException>(() => new CollisionCheckCommand(new Mock<ICollisionObject>().Object, null));
    }

    [Fact]
    public void Command_Successfull_exception_Execute()
    {
        var obj1 = new Mock<ICollisionObject>();
        var obj2 = new Mock<ICollisionObject>();
        IoC.Resolve<ICommand>("IoC.Register", "Collision.Strategy", (object[] args) => (object)true).Execute();

        var command = new CollisionCheckCommand(obj1.Object, obj2.Object);

        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void Command_Successfull_none_Execute()
    {
        var obj1 = new Mock<ICollisionObject>();
        var obj2 = new Mock<ICollisionObject>();
        IoC.Resolve<ICommand>("IoC.Register", "Collision.Strategy", (object[] args) => (object)false).Execute();

        var command = new CollisionCheckCommand(obj1.Object, obj2.Object);

        command.Execute();
    }
}
