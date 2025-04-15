using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests;

public class IoCRegGridOperationsCommandTest
{
    public IoCRegGridOperationsCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();
        new IoCRegGridOperationsCommand().Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Rules.GridWidth", (object[] args) => (object)10).Execute();
    }

    [Fact]
    public void NothingAddAndNothingReturn()
    {
        var grids = IoC.Resolve<GridFieldStructure[]>("Collision.Grid.Grids");
        Assert.Equal([], grids);
    }

    [Fact]
    public void AddGridAndGetIt()
    {
        var gf = IoC.Resolve<GridFieldStructure>("GridField", 1);
        IoC.Resolve<ICommand>("Collision.Grid.Grids.Add", gf).Execute();

        var got = IoC.Resolve<GridFieldStructure[]>("Collision.Grid.Grids");

        Assert.Equal([gf], got);
    }

    [Fact]
    public void AddObjectToGridAndGetIt()
    {
        Mock<IMovable> movable = new Mock<IMovable>();
        movable.SetupGet(m => m.Position).Returns(new Vector([0, 0]));

        var gf = IoC.Resolve<GridFieldStructure>("GridField", 1);
        IoC.Resolve<ICommand>("Collision.Grid.Grids.Add", gf).Execute();
        IoC.Resolve<ICommand>("Collision.Grid.AddObject", movable.Object).Execute();

        var got = IoC.Resolve<GridFieldStructure[]>("Collision.Grid.Grids")[0].Get(new Vector([0, 0]));
        Assert.Equal([movable.Object], got);
    }

    [Fact]
    public void TryToGetFromNothingThrows()
    {
        var gf = IoC.Resolve<GridFieldStructure>("GridField", 1);
        IoC.Resolve<ICommand>("Collision.Grid.Grids.Add", gf).Execute();

        Assert.Throws<KeyNotFoundException>(() =>
            IoC.Resolve<GridFieldStructure[]>("Collision.Grid.Grids")[0].Get(new Vector([0, 0])));
    }
}