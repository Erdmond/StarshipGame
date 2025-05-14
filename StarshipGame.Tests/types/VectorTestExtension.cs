using Xunit;

namespace StarshipGame.Tests;

public class VectorTests
{
    [Fact]
    public void Equals_ReturnsFalse_WhenObjectIsNotVector()
    {
        var vector = new Vector(new[] { 1, 2, 3 });
        var notAVector = new object();

        Assert.False(vector.Equals(notAVector));
    }
}
