using Moq;


namespace StarshipGame.Test;

public class UnitTestMove
{
    [Fact]
    public void TestNormalTranslation() // (12, 5) + (-7, 3) = (5, 8)
    {
        Vector initialPosition = new Vector(new int[] { 13, 5 });
        Vector velocity = new Vector(new int[] { -8, 3 });
        Vector expectedPosition = new Vector(new int[] { 5, 8 });

        Mock<IMovable> mock = new Mock<IMovable>();

        mock.SetupProperty(m => m.Position, initialPosition);
        mock.SetupGet(m => m.Velocity).Returns(velocity);

        ICommand move = new MoveCommand(mock.Object);
        move.Execute();

        mock.VerifySet(m => m.Position = It.Is<Vector>(v =>
            v.Equals(expectedPosition)), Times.Once);
    }


    [Fact]
    public void TestCannotReadPosition() // невозможно прочитать положение
    {
        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Throws(new Exception("Cannot read position"));
        mock.SetupGet(m => m.Velocity).Returns(new Vector(new int[] { -7, 3 }));
        ICommand move = new MoveCommand(mock.Object);

        Assert.Throws<Exception>(move.Execute);
    }

    [Fact]
    public void TestCannotReadVelocity() // невозможно прочитать скорость 
    {
        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new Vector(new int[] { 12, 5 }));
        mock.SetupGet(m => m.Velocity).Throws(new Exception("Cannot read velocity"));
        ICommand move = new MoveCommand(mock.Object);

        Assert.Throws<Exception>(move.Execute);
    }

    [Fact]
    public void TestCannotSetPosition() // невозможно установить положение
    {
        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new Vector(new int[] { 12, 5 }));
        mock.SetupGet(m => m.Velocity).Returns(new Vector(new int[] { -7, 3 }));
        mock.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws(new Exception("Cannot set position"));
        ICommand move = new MoveCommand(mock.Object);

        Assert.Throws<Exception>(move.Execute);
    }
}
