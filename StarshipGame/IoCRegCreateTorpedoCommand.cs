namespace StarshipGame;
public class RegisterIoCDependencyTorpedoCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Commands.Create.Torpedo", (object[] args) => new Torpedo(((IMovable)args[0]).Position, ((IMovable)args[0]).Velocity)).Execute();
    }
}
