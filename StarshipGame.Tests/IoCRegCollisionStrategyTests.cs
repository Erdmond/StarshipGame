namespace StarshipGame.Tests;
using Xunit;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using CollisionTree = Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>;

public class RegisterIoCDependencyCollisionStrategyTests
{
    public RegisterIoCDependencyCollisionStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegistersStrategyCorrectly()
    {
        var collisionTree = new CollisionTree();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Collision.Tree", (object[] _) => (object)collisionTree).Execute();

        var pos1 = new Vector(new[] { 1, 2 });
        var vel1 = new Vector(new[] { 3, 4 });
        var pos2 = new Vector(new[] { 5, 6 });
        var vel2 = new Vector(new[] { 7, 8 });

        var mockObj1 = new Mock<ICollisionObject>();
        var mockObj2 = new Mock<ICollisionObject>();

        mockObj1.SetupGet(o => o.Position).Returns(pos1);
        mockObj1.SetupGet(o => o.Velocity).Returns(vel1);
        mockObj2.SetupGet(o => o.Position).Returns(pos2);
        mockObj2.SetupGet(o => o.Velocity).Returns(vel2);

        var registerCommand = new RegisterIoCDependencyCollisionStrategy();
        registerCommand.Execute();

        var result = IoC.Resolve<object>("Collision.Strategy", mockObj1.Object, mockObj2.Object);
        Assert.False((bool)result);
    }
}
