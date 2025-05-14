namespace StarshipGame.Tests;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

public class RegisterIoCDependencyActivatorCommandTests
{
    public RegisterIoCDependencyActivatorCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegistersActivatorDependency()
    {
        new RegisterIoCDependencyActivatorCommand().Execute();

        var activateCommand = IoC.Resolve<ICommand>("Commands.ActivateCommand", "");

        Assert.NotNull(activateCommand);
        Assert.IsType<ActivateCommand>(activateCommand);
    }
}
