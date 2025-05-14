namespace StarshipGame.Tests;

using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

public class RegisterIoCDependencyAdapterIsComputedTests
{
    public RegisterIoCDependencyAdapterIsComputedTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var newScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", newScope).Execute();

        var dict = new Dictionary<(string, bool), string> { { ("FieldName", true), "SomeMethod" } };
        IoC.Resolve<ICommand>("IoC.Register", "Commands.ParseAttributes", (object[] args) => dict).Execute();

        new IoCRegAdapterIsComputed().Execute();
    }

    [Fact]
    public void AdapterIsComputedReturnsTrueWhenKeyExists()
    {
        var result = IoC.Resolve<object>("Adapter.IsComputed", "FieldName", true);

        Assert.True((bool)result);
    }

    [Fact]
    public void AdapterIsComputedReturnsFalseWhenKeyDoesNotExist()
    {
        var result = IoC.Resolve<object>("Adapter.IsComputed", "MissingField", false);

        Assert.False((bool)result);
    }
}
