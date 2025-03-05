namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

public class RegisterAuthCommandTests
{
    public RegisterAuthCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegistersAuthCommandDependency()
    {
        var registerCommand = new RegisterAuthCommand();
        registerCommand.Execute();

        var playerInfo = new Mock<IObjectInfo>().Object;
        var targetInfo = new Mock<IObjectInfo>().Object;

        var authCommand = IoC.Resolve<ICommand>("Commands.Auth", playerInfo, targetInfo);

        Assert.NotNull(authCommand);
        Assert.IsType<AuthCommand>(authCommand);
    }
} 
