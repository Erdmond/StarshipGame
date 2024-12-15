namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyActionsStart
{
    public RegisterIoCDependencyActionsStart()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

    }

    [Fact]
    public void Execute_ShouldResolveDependency_WhenInvoked()
    {
        var registerCommand = new RegisterIoCDependencyActionsStop();
        registerCommand.Execute();

        var mockDictionary = new Mock<IDictionary>();
        
        var actionStart = IoC.Resolve<StarshipGame.ICommand>("Actions.Start", mockDictionary.Object);

        Assert.NotNull(actionStart);
        Assert.IsAssignableFrom<ICommand>(actionStart);
    }
}
