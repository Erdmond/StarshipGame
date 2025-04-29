namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using System.Reflection;
/*
public class IoCRegParseAttributesTests
{
    public IoCRegParseAttributesTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    private class TestClass
    {
        [CustomMethod("TestCommand", true)]
        public void TestMethod() {}
    }

    [Fact]
    public void Execute_ShouldRegisterAttributesDictionary()
    {
        var command = new IoCRegParseAttributes();

        command.Execute();

        var registeredCommand = IoC.Resolve<IDictionary<(string, bool), string>>("Commands.ParseAttributes");
        Assert.NotNull(registeredCommand);
        Assert.NotEmpty(registeredCommand);
    }

    [Fact]
    public void Execute_ShouldContainCorrectMethodInfo()
    {
        // Arrange
        var command = new IoCRegParseAttributes();
        var expectedKey = ("TestCommand", true);
        var expectedValue = $"{typeof(TestClass).Name}.TestMethod";

        // Act
        command.Execute();
        var result = IoC.Resolve<IDictionary<(string, bool), string>>("Commands.ParseAttributes");

        // Assert
        Assert.Contains(expectedKey, result.Keys);
        Assert.Equal(expectedValue, result[expectedKey]);
    }

    [Fact]
    public void Execute_ShouldHandleMultipleAttributes()
    {
        // Arrange
        var command = new IoCRegParseAttributes();

        // Act
        command.Execute();
        var result = IoC.Resolve<IDictionary<(string, bool), string>>("Commands.ParseAttributes");

        // Assert
        Assert.True(result.Count >= 1); // Минимум одна регистрация из TestClass
    }

    [Fact]
    public void Execute_ShouldGroupByCompositeKey()
    {
        // Arrange
        var command = new IoCRegParseAttributes();
        
        // Act
        command.Execute();
        var result = IoC.Resolve<IDictionary<(string, bool), string>>("Commands.ParseAttributes");
        
        // Assert
        var key = ("TestCommand", true);
        Assert.Single(result.Where(kv => kv.Key == key));
    }
}

// Вспомогательный класс для регистрации зависимостей
public class RegisterDependencyCommand : Hwdtech.ICommand
{
    private readonly string _key;
    private readonly object _dependency;
    
    public RegisterDependencyCommand(string key, object dependency)
    {
        _key = key;
        _dependency = dependency;
    }
    
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            _key,
            (object[] args) => _dependency
        ).Execute();
    }
}
*/