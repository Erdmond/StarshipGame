using Moq;
using StarshipGame;

namespace StarshipGame.Test;

public class RotateTest
{
    [Fact]
    public void TestNormalRotation() // 45 + 90 = 135
    {
        Mock<IRotatable> mock = new Mock<IRotatable>();
        mock.SetupGet(m => m.Angle).Returns(() => 45);
        mock.SetupGet(m => m.AngleVelocity).Returns(() => 90);
        ICommand rotate = new RotateCommand(mock.Object);

        rotate.Execute();

        mock.VerifySet(m => m.Angle = 135, Times.Once);
    }

    [Fact]
    public void TestCannotReadAngle() // невозможно прочитать угол
    {
        Mock<IRotatable> mock = new Mock<IRotatable>();
        mock.SetupGet(m => m.Angle).Throws(new Exception("Cannot read angle"));
        mock.SetupGet(m => m.AngleVelocity).Returns(() => 90);
        ICommand rotate = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotReadAngleVelocity() // невозможно прочитать скорость
    {
        Mock<IRotatable> mock = new Mock<IRotatable>();
        mock.SetupGet(m => m.Angle).Returns(() => 45);
        mock.SetupGet(m => m.AngleVelocity).Throws(new Exception("Cannot read velocity"));
        ICommand rotate = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotSetAngle() // невозможно установить угол
    {
        Mock<IRotatable> mock = new Mock<IRotatable>();
        mock.SetupGet(m => m.Angle).Returns(() => 45);
        mock.SetupGet(m => m.AngleVelocity).Returns(() => 90);
        mock.SetupSet(m => m.Angle = It.IsAny<int>()).Throws(new Exception("Cannot set angle"));
        ICommand rotate = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }
}