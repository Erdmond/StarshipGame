namespace StarshipGame.Test;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyTorpedoCommandTest
{
    public RegisterIoCDependencyTorpedoCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegisterIoCDependencyTorpedoCommand()
    {
        var registerCommand = new RegisterIoCDependencyTorpedoCommand();
        registerCommand.Execute();

        var movablyMock = new Mock<IMovable>();

        var createTorpedo = IoC.Resolve<IMovable>("Commands.Create.Torpedo", movablyMock.Object);

        Assert.NotNull(createTorpedo);
        Assert.IsType<IMovable>(createTorpedo);
    }
}
