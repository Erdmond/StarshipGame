namespace StarshipGame.Tests;

using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

public class RegisterIoCDependencyRotateCommandTest
{
    public RegisterIoCDependencyRotateCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Adapters.IRotatable", (object[] args) => (new Mock<IRotatable>().Object)).Execute();
    }

    [Fact]
    public void Execute_RegistersRotateCommandDependency()
    {
        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();

        var rotatableMock = new Mock<IRotatable>();

        var rotateCommand = IoC.Resolve<ICommand>("Commands.Rotate", rotatableMock.Object);

        Assert.NotNull(rotateCommand);
        Assert.IsType<RotateCommand>(rotateCommand);
    }
}