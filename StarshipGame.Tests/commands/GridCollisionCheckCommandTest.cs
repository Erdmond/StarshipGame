using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests.commands;

public class GridCollisionCheckCommandTest
{
    public GridCollisionCheckCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();
    }

    [Fact]
    public void ObjectDontMoveNothingHappen()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();

        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Velocity).Returns(new Vector([0, 0]));
        ICommand cmd = new GridCollisionCheckCommand(mock.Object, new GridFieldStructure(0));

        cmd.Execute();
    }

    [Fact]
    public void GridWidthIsNotSpecifiedThrows()
    {
        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Velocity).Returns(new Vector([0, 0]));

        Assert.Throws<ArgumentException>(() => new GridCollisionCheckCommand(mock.Object, new GridFieldStructure(0)));
    }

    [Fact]
    public void ObjectNotAddKeyAccept()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Collision.Strategy", (object[] args) => (object)false).Execute();
        Mock<IMovable> mock1 = new Mock<IMovable>();
        mock1.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock1.SetupGet(m => m.Position).Returns(new Vector([1, 1]));
        Mock<IMovable> mock2 = new Mock<IMovable>();
        mock2.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock2.SetupGet(m => m.Position).Returns(new Vector([5, 5]));

        Assert.Throws<KeyNotFoundException>(() =>
            new GridCollisionCheckCommand(mock2.Object, new GridFieldStructure(0)).Execute());
    }

    [Fact]
    public void ObjectsInOneCellAndDontCollideAccept()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Collision.Strategy", (object[] args) => (object)false).Execute();
        Mock<IMovable> mock1 = new Mock<IMovable>();
        mock1.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock1.SetupGet(m => m.Position).Returns(new Vector([1, 1]));
        Mock<IMovable> mock2 = new Mock<IMovable>();
        mock2.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock2.SetupGet(m => m.Position).Returns(new Vector([5, 5]));

        new IoCRegGridOperationsCommand().Execute();
        var gf = IoC.Resolve<GridFieldStructure>("GridField", 1);
        gf.Add(mock1.Object);
        gf.Add(mock2.Object);

        new GridCollisionCheckCommand(mock2.Object, gf).Execute();
    }

    [Fact]
    public void ObjectsInDifferentCellAndDontCollideAccept()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Collision.Strategy", (object[] args) => (object)false).Execute();
        Mock<IMovable> mock1 = new Mock<IMovable>();
        mock1.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock1.SetupGet(m => m.Position).Returns(new Vector([1, 1]));
        Mock<IMovable> mock2 = new Mock<IMovable>();
        mock2.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock2.SetupGet(m => m.Position).Returns(new Vector([100, 100]));

        new IoCRegGridOperationsCommand().Execute();
        var gf = IoC.Resolve<GridFieldStructure>("GridField", 1);
        gf.Add(mock1.Object);
        gf.Add(mock2.Object);

        new GridCollisionCheckCommand(mock2.Object, gf).Execute();
    }

    [Fact]
    public void ObjectsInOneCellAndCollideThrows()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Collision.Strategy", (object[] args) =>
        {
            throw new Exception("Collision");
            return (object)false; // просто для типов для IoC-a
        }).Execute();

        Mock<IMovable> mock1 = new Mock<IMovable>();
        mock1.SetupGet(m => m.Velocity).Returns(new Vector([1, 1]));
        mock1.SetupGet(m => m.Position).Returns(new Vector([1, 1]));
        Mock<IMovable> mock2 = new Mock<IMovable>();
        mock2.SetupGet(m => m.Velocity).Returns(new Vector([-1, -1]));
        mock2.SetupGet(m => m.Position).Returns(new Vector([2, 2]));

        new IoCRegGridOperationsCommand().Execute();
        var gf = IoC.Resolve<GridFieldStructure>("GridField", 1);
        gf.Add(mock1.Object);
        gf.Add(mock2.Object);

        Assert.Throws<Exception>(() => new GridCollisionCheckCommand(mock2.Object, gf).Execute());
    }
}