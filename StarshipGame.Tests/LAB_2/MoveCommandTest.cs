using Moq;


namespace StarshipGame.Test;

public class UnitTestMove
{
    [Fact]
    public void TestNormalTranslation() // (12, 5) + (-7, 3) = (5, 8)
    {
        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new int[] { 12, 5 });
        mock.SetupGet(m => m.Velocity).Returns(new int[] { -7, 3 });
        ICommand move = new MoveCommand(mock.Object);

        move.Execute();

        mock.VerifySet(m => m.Position = new int[] { 5, 8 }, Times.Once);
    }

    [Fact]
    public void TestCannotReadPosition() // невозможно прочитать положение
    {
        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Throws(new Exception("Cannot read position"));
        mock.SetupGet(m => m.Velocity).Returns(new int[] { -7, 3 });
        ICommand move = new MoveCommand(mock.Object);

        Assert.Throws<Exception>(move.Execute);
    }

    [Fact]
    public void TestCannotReadVelocity() // невозможно прочитать скорость 
    {
        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new int[] { 12, 5 });
        mock.SetupGet(m => m.Velocity).Throws(new Exception("Cannot read velocity"));
        ICommand move = new MoveCommand(mock.Object);

        Assert.Throws<Exception>(move.Execute);
    }

    [Fact]
    public void TestCannotSetPosition() // невозможно установить положение
    {
        Mock<IMovable> mock = new Mock<IMovable>();
        mock.SetupGet(m => m.Position).Returns(new int[] { 12, 5 });
        mock.SetupGet(m => m.Velocity).Returns(new int[] { -7, 3 });
        mock.SetupSet(m => m.Position = It.IsAny<int[]>()).Throws(new Exception("Cannot set position"));
        ICommand move = new MoveCommand(mock.Object);

        Assert.Throws<Exception>(move.Execute);
    }
}