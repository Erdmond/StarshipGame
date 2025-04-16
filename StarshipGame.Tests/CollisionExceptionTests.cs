using Xunit;
using Moq;
namespace StarshipGame.Tests;

public class CollisionDetectedExceptionTests
{
    [Fact]
    public void Constructor_SetsPropertiesAndMessage_Correctly()
    {
        var mockObj1 = new Mock<ICollisionObject>();
        var mockObj2 = new Mock<ICollisionObject>();

        mockObj1.Setup(o => o.Position).Returns(new Vector(new[] { 1, 2 }));
        mockObj2.Setup(o => o.Position).Returns(new Vector(new[] { 3, 4 }));

        var exception = new CollisionDetectedException(mockObj1.Object, mockObj2.Object);

        Assert.Same(mockObj1.Object, exception.Object1);
        Assert.Same(mockObj2.Object, exception.Object2);
        Assert.Equal("Collision detected between objects at positions (1, 2) and (3, 4)", exception.Message);
    }
}
