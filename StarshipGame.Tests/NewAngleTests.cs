using Xunit;  
namespace StarshipGame.Tests  
{  
    public class NewAngleTests  
    {  
        [Fact]  
        public void Equals_ReturnsFalse_WhenNull()  
        {  
            var angle = new Angle(3);  
            Assert.False(angle.Equals(null));  
        }  
  
        [Fact]  
        public void Equals_ReturnsFalse_WhenDifferentType()  
        {  
            var angle = new Angle(3);  
            Assert.False(angle.Equals("Not Angle"));  
        }  
  
        [Fact]  
        public void Equals_ReturnsTrue_WhenEqual()  
        {  
            var angle1 = new Angle(10);  
            var angle2 = new Angle(2);  
  
            Assert.True(angle1.Equals(angle2));  
        }  
  
        [Fact]  
        public void Equals_ReturnsFalse_WhenDifferent()  
        {  
            var angle1 = new Angle(3);  
            var angle2 = new Angle(4);  
  
            Assert.False(angle1.Equals(angle2));  
        }  
  
        [Fact]  
        public void ToRadians_ReturnsZero_ForZeroAngle()  
        {  
            var angle = new Angle(0);  
            double radians = angle;  
  
            Assert.Equal(0, radians, precision: 5);  
        }  
  
        [Fact]  
        public void ToRadians_ReturnsPiOver4_ForOneEighth()  
        {  
            var angle = new Angle(1);  
            double radians = angle;  
  
            Assert.Equal(Math.PI / 4, radians, precision: 5);  
        }  
  
        [Fact]  
        public void ToRadians_ReturnsPi_ForHalfCircle()  
        {  
            var angle = new Angle(4);  
            double radians = angle;  
  
            Assert.Equal(Math.PI, radians, precision: 5);  
        }  
        private const double Precision = 1e-5;  
  
        [Fact]  
        public void Cos_ReturnsOne_ForZeroAngle()  
        {  
            var angle = new Angle(0);  
            double cosValue = angle.Cos();  
  
            Assert.Equal(1.0, cosValue, Precision);  
        }  
  
        [Fact]  
        public void Sin_ReturnsOne_ForQuarterCircle()  
        {  
            var angle = new Angle(2);  
            double sinValue = angle.Sin();  
  
            Assert.Equal(1.0, sinValue, Precision);  
        }  
  
        [Fact]  
        public void Tan_ReturnsOne_ForEighthCircle()  
        {  
            var angle = new Angle(1);  
            double tanValue = angle.Tan();  
  
            Assert.Equal(1.0, tanValue, Precision);  
        }  
  
        [Fact]  
        public void Sec_ReturnsOne_ForZeroAngle()  
        {  
            var angle = new Angle(0);  
            double secValue = angle.Sec();  
  
            Assert.Equal(1.0, secValue, Precision);  
        }  
  
        [Fact]  
        public void Csc_ReturnsOne_ForQuarterCircle()  
        {  
            var angle = new Angle(2);  
            double cscValue = angle.Csc();  
  
            Assert.Equal(1.0, cscValue, Precision);  
        }  
  
        [Fact]  
        public void Cot_ReturnsOne_ForEighthCircle()  
        {  
            var angle = new Angle(1);  
            double cotValue = angle.Cot();  
  
            Assert.Equal(1.0, cotValue, Precision);  
        }  
    }  
}
