namespace StarshipGame;
using Hwdtech;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Actions.Stop", (object[] args) => new CmdEndCommand(IoC.Resolve<Hwdtech.ICommand>("Some.Default.Command"), (IOrder)args[0])).Execute();
    }
}
