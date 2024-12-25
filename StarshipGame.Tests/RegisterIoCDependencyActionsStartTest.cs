namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyActionsStartTest
{
    public RegisterIoCDependencyActionsStartTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Macro", (object[] args) => (new Mock<ICommand>().Object)).Execute();
    }

    [Fact]
    public void Execute_ShouldResolveDependency_WhenInvoked()
    {
        var registerCommand = new RegisterIoCDependencyActionsStart();
        registerCommand.Execute();

        var mockDictionary = new Mock<IDictionary<string, object>>();

        var actionStart = IoC.Resolve<Hwdtech.ICommand>("Actions.Start", mockDictionary.Object);

        Assert.NotNull(actionStart);
        Assert.IsAssignableFrom<Hwdtech.ICommand>(actionStart);
    }
}
