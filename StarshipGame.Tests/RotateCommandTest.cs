using Moq;

namespace StarshipGame.Test;

public class RotateTest
{
    [Fact]
    public void TestNormalRotation() // 45 + 45 = 90
    {
        Mock<IRotatable> mock = new Mock<IRotatable>();
        mock.SetupGet(m => m.Angle).Returns(() => new Angle(45));
        mock.SetupGet(m => m.AngleVelocity).Returns(() => new Angle(45));
        Hwdtech.ICommand rotate = new RotateCommand(mock.Object);

        rotate.Execute();

        mock.VerifySet(m => m.Angle = new Angle(90), Times.Once);
    }

    [Fact]
    public void TestCannotReadAngle() // невозможно прочитать угол
    {
        Mock<IRotatable> mock = new Mock<IRotatable>();
        mock.SetupGet(m => m.Angle).Throws(new Exception("Cannot read angle"));
        mock.SetupGet(m => m.AngleVelocity).Returns(() => new Angle(90));
        Hwdtech.ICommand rotate = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotReadAngleVelocity() // невозможно прочитать скорость
    {
        Mock<IRotatable> mock = new Mock<IRotatable>();
        mock.SetupGet(m => m.Angle).Returns(() => new Angle(45));
        mock.SetupGet(m => m.AngleVelocity).Throws(new Exception("Cannot read velocity"));
        Hwdtech.ICommand rotate = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }

    [Fact]
    public void TestCannotSetAngle() // невозможно установить угол
    {
        Mock<IRotatable> mock = new Mock<IRotatable>();
        mock.SetupGet(m => m.Angle).Returns(() => new Angle(45));
        mock.SetupGet(m => m.AngleVelocity).Returns(() => new Angle(45));
        mock.SetupSet(m => m.Angle = It.IsAny<Angle>()).Throws(new Exception("Cannot set angle"));
        Hwdtech.ICommand rotate = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(rotate.Execute);
    }
}
