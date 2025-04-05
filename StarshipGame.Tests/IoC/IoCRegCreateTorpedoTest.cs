namespace StarshipGame.Tests;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

public class IoCRegisterCreateTorpedoTests
{
    public IoCRegisterCreateTorpedoTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_RegisterCreateTorpedoDependency()
    {
        var registerCommand = new RegisterIoCDependencyCreateTorpedo();
        registerCommand.Execute();

        var position = new Vector(new int[] { 1, 2 });
        var velocity = new Vector(new int[] { 0, 1 });
        var objectId = "0";
        var playerId = "1";

        var createTorpedo = IoC.Resolve<IDictionary<string, object>>("Commands.Create.Torpedo", position, velocity, objectId, playerId);

        Assert.NotNull(createTorpedo);
        Assert.IsType<Dictionary<string, object>>(createTorpedo);
    }
}
