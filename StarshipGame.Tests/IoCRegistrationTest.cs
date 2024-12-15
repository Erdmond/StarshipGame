namespace StarshipGame.Tests;

using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

public class RegisterIoCDependencyRotateCommandTest
{
    public RegisterIoCDependencyRotateCommandTest()
    {
        // Initialize IoC container
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        // Register the dependency for IRotatable
        IoC.Resolve<ICommand>("IoC.Register", "Adapters.IRotatable", (object[] args) => (new Mock<IRotatable>().Object)).Execute();
    }

    [Fact]
    public void Execute_RegistersRotateCommandDependency()
    {
        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();

        // Mock the IRotatable object
        var rotatableMock = new Mock<IRotatable>();

        // Resolve and execute the rotate command
        var rotateCommand = IoC.Resolve<ICommand>("Commands.Rotate", rotatableMock.Object);

        Assert.NotNull(rotateCommand);
        Assert.IsType<RotateCommand>(rotateCommand);

        // Execute the rotate command
        rotateCommand.Execute();
    }
}