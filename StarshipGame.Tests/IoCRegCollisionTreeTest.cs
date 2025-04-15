namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;
using CollisionTree = Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>;

public class IoCRegCollisionTreeInitTests
{
    public IoCRegCollisionTreeInitTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegisterIoCDependencyGameCollisionTree()
    {
        var registerCommand = new RegisterIoCDependencyGameCollisionTreeCommand();
        registerCommand.Execute();

        var collisionTree = IoC.Resolve<CollisionTree>("Game.Collision.Tree");

        Assert.NotNull(collisionTree);
        Assert.IsType<CollisionTree>(collisionTree);
        Assert.NotEmpty(collisionTree);
    }
}
