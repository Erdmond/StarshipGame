namespace StarshipGame;
public class RegisterIoCDependencyTorpedoCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Commands.Create.Torpedo", (object[] args) => new Torpedo((Vector)args[0], (Vector)args[1])).Execute();
    }
}
