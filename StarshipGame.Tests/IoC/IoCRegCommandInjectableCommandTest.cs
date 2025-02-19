using Hwdtech;
using Hwdtech.Ioc;

namespace StarshipGame.Tests;

public class IoCCommandInjectableCommand
{
    public IoCCommandInjectableCommand()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void GenericVariantsOfCommandInjectableDoesNotExcept()
    {
        new RegisterCommandInjectableCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Commands.CommandInjectable");
        IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        IoC.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
    }
}
