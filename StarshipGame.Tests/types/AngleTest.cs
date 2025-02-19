namespace StarshipGame.Tests;
public class AngleTest
{
    [Fact]
    public void TestAdditionProducesCorrectAngle()
    {
        var angle1 = new Angle(5);
        var angle2 = new Angle(7);
        var result = angle1 + angle2;
        Assert.Equal(new Angle(4), result); // (5 + 7) % 8 = 4
    }

    [Fact]
    public void TestEqualsForEqualAngles()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);
        Assert.True(angle1.Equals(angle2)); // 15 % 8 == 23 % 8
    }

    [Fact]
    public void TestEqualityOperatorForEqualAngles()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);
        Assert.True(angle1 == angle2); // 15 % 8 == 23 % 8
    }

    [Fact]
    public void TestEqualsForUnequalAngles()
    {
        var angle1 = new Angle(1);
        var angle2 = new Angle(2);
        Assert.False(angle1.Equals(angle2)); // 1 % 8 != 2 % 8
    }

    [Fact]
    public void TestInequalityOperatorForUnequalAngles()
    {
        var angle1 = new Angle(1);
        var angle2 = new Angle(2);
        Assert.True(angle1 != angle2); // 1 != 2
    }

    [Fact]
    public void TestHashCodePresence()
    {
        var angle = new Angle(15);
        var hashCode = angle.GetHashCode();
        Assert.Equal(7, hashCode); // 15 % 8 == 7
    }
}
