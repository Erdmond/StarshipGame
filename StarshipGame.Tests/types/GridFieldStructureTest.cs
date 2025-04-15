using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests;

public class GridFieldStructureTest
{
    public GridFieldStructureTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();
    }

    [Fact]
    public void GridWidthNotSetException()
    {
        Assert.Throws<ArgumentException>(() => new GridFieldStructure(0));
    }

    [Fact]
    public void DontAddAndGetException()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        var gf = new GridFieldStructure(1);
        Assert.Throws<KeyNotFoundException>(() => gf.Get(new Vector([1, 1])));
    }

    [Fact]
    public void AddAndGetAccept()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        var gf = new GridFieldStructure(1);

        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new Vector([0, 0]));

        gf.Add(mock.Object);

        var got = gf.Get(new Vector([0, 0]));
        Assert.Equal([mock.Object], got);
    }

    [Fact]
    public void RecountWithoutMovementNothingChanges()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        var gf = new GridFieldStructure(1);

        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new Vector([0, 0]));
        mock.SetupGet(m => m.Velocity).Returns(new Vector([0, 0]));

        gf.Add(mock.Object);
        gf.RecountGridPosition(mock.Object);

        var got = gf.Get(new Vector([0, 0]));
        Assert.Equal([mock.Object], got);
    }

    [Fact]
    public void RecountWithoutLittleMovementNothingChanges()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        var gf = new GridFieldStructure(1);

        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new Vector([0, 0]));
        mock.SetupGet(m => m.Velocity).Returns(new Vector([5, 5]));

        gf.Add(mock.Object);
        gf.RecountGridPosition(mock.Object);

        var got = gf.Get(new Vector([0, 0]));
        Assert.Equal([mock.Object], got);
    }

    [Fact]
    public void RecountWithoutBigMovementChanges()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        var gf = new GridFieldStructure(1);

        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new Vector([0, 0]));
        mock.SetupGet(m => m.Velocity).Returns(new Vector([50, 50]));

        gf.Add(mock.Object);

        // допустим, он переместился
        mock.SetupGet(m => m.Position).Returns(new Vector([50, 50]));
        gf.RecountGridPosition(mock.Object);

        var got = gf.Get(new Vector([50, 50]));
        Assert.Equal([mock.Object], got);

        var before = gf.Get(new Vector([0, 0]));
        Assert.Equal([], before);
    }

    [Fact]
    public void RecountWithoutVelocityThrows()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        var gf = new GridFieldStructure(1);

        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new Vector([0, 0]));

        gf.Add(mock.Object);
        Assert.Throws<ArgumentNullException>(() => gf.RecountGridPosition(mock.Object));
    }

    [Fact]
    public void RemoveAndNothingInGridField()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
        var gf = new GridFieldStructure(1);

        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new Vector([0, 0]));

        gf.Add(mock.Object);
        gf.Remove(mock.Object);

        var got = gf.Get(new Vector([0, 0]));
        Assert.Equal([], got);
    }
}