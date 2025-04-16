namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyCollisionExceptionTests
{
    public RegisterIoCDependencyCollisionExceptionTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegistersCollisionExceptionCorrectly()
    {
        var registerCommand = new RegisterIoCDependencyCollisionException();
        registerCommand.Execute();

        var obj1 = new Mock<ICollisionObject>();
        var obj2 = new Mock<ICollisionObject>();

        obj1.Setup(o => o.Position).Returns(new Vector(new[] {1, 2}));
        obj2.Setup(o => o.Position).Returns(new Vector(new[] {3, 4}));

        var exception = IoC.Resolve<CollisionDetectedException>("Exceptions.Collision", obj1.Object, obj2.Object);

        Assert.NotNull(exception);
        Assert.IsType<CollisionDetectedException>(exception);
        Assert.Same(obj1.Object, exception.Object1);
        Assert.Same(obj2.Object, exception.Object2);
        Assert.Equal("Collision detected between objects at positions (1, 2) and (3, 4)", exception.Message);
    }
}
