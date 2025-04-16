using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests;

public class IoCRegGridCollisionCheckCommandTest
{
    public IoCRegGridCollisionCheckCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();
        new IoCRegGridOperationsCommand().Execute();
        new IoCRegGridCollisionCheckCommand().Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
    }

    [Fact]
    public void ObjectsInOneCellAndDontCollideAccept()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Commands.CollisionCheck", (object[] args) => (object)false).Execute();

        Mock<IMovable> mock1 = new Mock<IMovable>();
        mock1.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock1.SetupGet(m => m.Position).Returns(new Vector([1, 1]));
        Mock<IMovable> mock2 = new Mock<IMovable>();
        mock2.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock2.SetupGet(m => m.Position).Returns(new Vector([5, 5]));

        var gf = IoC.Resolve<GridFieldStructure>("GridField", 1);

        IoC.Resolve<ICommand>("Collision.Grid.Grids.Add", gf).Execute();
        IoC.Resolve<ICommand>("Collision.Grid.AddObject", mock1.Object).Execute();
        IoC.Resolve<ICommand>("Collision.Grid.AddObject", mock2.Object).Execute();

        IoC.Resolve<ICommand>("Collision.Grid.Check", mock1.Object).Execute();
    }

    [Fact]
    public void GridNotAddThrowsException()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Commands.CollisionCheck", (object[] args) => (object)false).Execute();

        Mock<IMovable> mock1 = new Mock<IMovable>();
        mock1.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock1.SetupGet(m => m.Position).Returns(new Vector([1, 1]));
        Mock<IMovable> mock2 = new Mock<IMovable>();
        mock2.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock2.SetupGet(m => m.Position).Returns(new Vector([5, 5]));

        IoC.Resolve<ICommand>("Collision.Grid.AddObject", mock1.Object).Execute();
        IoC.Resolve<ICommand>("Collision.Grid.AddObject", mock2.Object).Execute();

        Assert.Throws<Exception>(() => IoC.Resolve<ICommand>("Collision.Grid.Check", mock1.Object).Execute());
    }

    [Fact]
    public void OneObjectAndDontCollideAccept()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Commands.CollisionCheck", (object[] args) => (object)false).Execute();

        Mock<IMovable> mock1 = new Mock<IMovable>();
        mock1.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock1.SetupGet(m => m.Position).Returns(new Vector([1, 1]));

        var gf = IoC.Resolve<GridFieldStructure>("GridField", 1);

        IoC.Resolve<ICommand>("Collision.Grid.Grids.Add", gf).Execute();
        IoC.Resolve<ICommand>("Collision.Grid.AddObject", mock1.Object).Execute();

        IoC.Resolve<ICommand>("Collision.Grid.Check", mock1.Object).Execute();
    }
}
