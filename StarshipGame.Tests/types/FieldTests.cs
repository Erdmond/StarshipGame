using StarshipGame;
namespace StarshipGame.Tests;

public class FieldTests
{
    [Theory]
    [InlineData("string", "UserName", true, false, "GetUser", null, true)]
    [InlineData("int", "Age", false, true, null, "SetAge", false)]
    [InlineData("bool", "IsActive", true, true, null, null, true)]
    [InlineData("", "", false, false, "", "", false)]
    public void Constructor_InitializesPropertiesCorrectly(
        string type,
        string name,
        bool isDefaultGetter,
        bool isDefaultSetter,
        string? customGetter,
        string? customSetter,
        bool needSetter)
    {
        var field = new Field(type, name, isDefaultGetter, isDefaultSetter, customGetter, customSetter, needSetter);

        Assert.Equal(type, field.Type);
        Assert.Equal(name, field.Name);
        Assert.Equal(isDefaultGetter, field.IsDefaultGetter);
        Assert.Equal(isDefaultSetter, field.IsDefaultSetter);
        Assert.Equal(customGetter, field.CustomGetter);
        Assert.Equal(customSetter, field.CustomSetter);
        Assert.Equal(needSetter, field.NeedSetter);
    }

    [Fact]
    public void Properties_ShouldHandleNullCustomAccessors()
    {
        var field = new Field("string", "Data", true, true, null, null, false);

        Assert.Null(field.CustomGetter);
        Assert.Null(field.CustomSetter);
    }

    [Fact]
    public void Properties_ShouldHandleEmptyStrings()
    {
        var field = new Field("", "", false, false, "", "", true);

        Assert.Equal(string.Empty, field.Type);
        Assert.Equal(string.Empty, field.Name);
        Assert.Equal(string.Empty, field.CustomGetter);
        Assert.Equal(string.Empty, field.CustomSetter);
    }

    [Fact]
    public void BooleanProperties_ShouldHandleFalseValues()
    {
        var field = new Field("int", "Count", false, false, null, null, false);

        Assert.False(field.IsDefaultGetter);
        Assert.False(field.IsDefaultSetter);
        Assert.False(field.NeedSetter);
    }
}
