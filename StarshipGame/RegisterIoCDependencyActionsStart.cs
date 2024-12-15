namespace StarshipGame;
using Hwdtech;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "Actions.Start",
            (object[] args) => args[0]
        ).Execute();
    }
}
