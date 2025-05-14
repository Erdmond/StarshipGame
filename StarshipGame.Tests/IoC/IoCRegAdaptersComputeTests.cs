namespace StarshipGame.Tests;

using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

public class RegisterIoCDependencyAdapterComputeMethodTests
{
    public RegisterIoCDependencyAdapterComputeMethodTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var newScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", newScope).Execute();

        var dict = new Dictionary<(string, bool), string> { { ("FieldName", true), "SomeMethod" } };
        IoC.Resolve<ICommand>("IoC.Register", "Commands.ParseAttributes", (object[] args) => dict).Execute();

        new IoCRegAdapterComputeMethod().Execute();
    }

    [Fact]
    public void AdapterComputeMethodReturnsCorrectMethodNameWhenKeyExists()
    {
        var result = IoC.Resolve<object>("Adapter.ComputeMethod", "FieldName", true);

        Assert.Equal("SomeMethod", result);
    }

    [Fact]
    public void AdapterComputeMethodThrowsWhenKeyDoesNotExist()
    {
        Assert.Throws<KeyNotFoundException>(() =>
            IoC.Resolve<object>("Adapter.ComputeMethod", "MissingField", false));
    }
}
