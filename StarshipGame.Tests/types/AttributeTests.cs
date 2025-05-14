namespace StarshipGame.Tests;
using System.Reflection;

public class CustomMethodAttributeTests
{
    [Fact]
    public void AttributeUsage_ShouldBeCorrect()
    {
        var attributeType = typeof(CustomMethodAttribute);

        var attributeUsage = attributeType.GetCustomAttributes<AttributeUsageAttribute>().FirstOrDefault();

        Assert.NotNull(attributeUsage);
        Assert.Equal(AttributeTargets.Method, attributeUsage.ValidOn);
        Assert.False(attributeUsage.AllowMultiple);
        Assert.True(attributeUsage.Inherited);
    }

    [Theory]
    [InlineData("TestName", true)]
    [InlineData("AnotherName", false)]
    [InlineData("", true)] // Проверка на пустое имя
    public void Constructor_ShouldInitializePropertiesCorrectly(string name, bool isGet)
    {
        var attribute = new CustomMethodAttribute(name, isGet);

        Assert.Equal(name, attribute.Name);
        Assert.Equal(isGet, attribute.IsGet);
    }

    [Fact]
    public void Attribute_ShouldApplyToMethods()
    {
        var testClassType = typeof(TestClass);
        var methodInfo = testClassType.GetMethod("TestMethod");

        var attributes = methodInfo.GetCustomAttributes<CustomMethodAttribute>(false).ToList();

        Assert.Single(attributes);
        var attribute = attributes.First();
        Assert.Equal("SampleMethod", attribute.Name);
        Assert.True(attribute.IsGet);
    }

    private class TestClass
    {
        [CustomMethod("SampleMethod", true)]
        public void TestMethod() { }
    }
}
