namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyActionsStopTests
{
    public RegisterIoCDependencyActionsStopTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Some.Default.Command", (object[] args) => new Mock<ICommand>().Object).Execute();
    }

    [Fact]
    public void Execute_ShouldResolveDependency_WhenInvoked()
    {
        var registerCommand = new RegisterIoCDependencyActionsStop();
        registerCommand.Execute(); 

        var cmd = new Mock<Hwdtech.ICommand>();
        var mockDictionary = new Mock<IDictionary>();

        var actionStop = IoC.Resolve<Hwdtech.ICommand>("Actions.Stop", cmd.Object, mockDictionary.Object);

        Assert.NotNull(actionStop);
        Assert.IsType<CmdEndCommand>(actionStop);
    }
}
