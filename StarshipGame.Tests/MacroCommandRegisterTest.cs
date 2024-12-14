using Hwdtech;
using Hwdtech.Ioc;

namespace StarshipGame.Tests;

public class MacroCommandRegisterTest
{
    public MacroCommandRegisterTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }


    [Fact]
    public void RegisterIoCDependencyMacroCommandExecuteAndDependencyResolved()
    {
        new RegisterIoCDependencyMacroCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Commands.Macro");
    }
}
