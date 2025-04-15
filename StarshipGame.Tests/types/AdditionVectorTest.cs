using Xunit;
using StarshipGame;

namespace StarshipGame.Tests
{
    public class VectorExtensionsTests
    {
        [Fact]
        public void Scale_ReturnsCorrectVector_WhenPositiveScale()
        {
            var vector = new Vector(new[] { 2, 3 });
            var scaled = vector.Scale(2);

            Assert.Equal(4, scaled.GetCoordinate(0));
            Assert.Equal(6, scaled.GetCoordinate(1));
        }

        [Fact]
        public void Scale_ReturnsCorrectVector_WhenNegativeScale()
        {
            var vector = new Vector(new[] { 1, -4 });
            var scaled = vector.Scale(-3);

            Assert.Equal(-3, scaled.GetCoordinate(0));
            Assert.Equal(12, scaled.GetCoordinate(1));
        }

        [Fact]
        public void Scale_ReturnsZeroVector_WhenScaleIsZero()
        {
            var vector = new Vector(new[] { 5, 7 });
            var scaled = vector.Scale(0);

            Assert.Equal(0, scaled.GetCoordinate(0));
            Assert.Equal(0, scaled.GetCoordinate(1));
        }

        [Fact]
        public void GetCoordinate_ReturnsCorrectValue_ForValidIndex()
        {
            var vector = new Vector(new[] { 10, 20, 30 });

            Assert.Equal(10, vector.GetCoordinate(0));
            Assert.Equal(20, vector.GetCoordinate(1));
            Assert.Equal(30, vector.GetCoordinate(2));
        }

        [Fact]
        public void GetCoordinate_ThrowsIndexOutOfRange_WhenIndexNegative()
        {
            var vector = new Vector(new[] { 1, 2 });

            var ex = Assert.Throws<IndexOutOfRangeException>(() => vector.GetCoordinate(-1));
            Assert.Contains("Индекс -1 выходит за границы вектора", ex.Message);
        }

        [Fact]
        public void GetCoordinate_ThrowsIndexOutOfRange_WhenIndexTooLarge()
        {
            var vector = new Vector(new[] { 1, 2 });

            var ex = Assert.Throws<IndexOutOfRangeException>(() => vector.GetCoordinate(2));
            Assert.Contains("Индекс 2 выходит за границы вектора", ex.Message);
        }

        [Fact]
        public void Scale_ThrowsArgumentNull_WhenVectorIsNull()
        {
            Vector vector = null;

            var ex = Assert.Throws<ArgumentNullException>(() => vector.Scale(5));
            Assert.Equal("vector", ex.ParamName);
        }

        [Fact]
        public void GetCoordinate_ThrowsArgumentNull_WhenVectorIsNull()
        {
            Vector vector = null;

            var ex = Assert.Throws<ArgumentNullException>(() => vector.GetCoordinate(0));
            Assert.Equal("vector", ex.ParamName);
        }

        [Fact]
        public void Scale_WorksCorrectly_WithEmptyVector()
        {
            var vector = new Vector(new int[0]);
            var scaled = vector.Scale(5);

            Assert.Throws<IndexOutOfRangeException>(() => scaled.GetCoordinate(0));
        }
    }
}
