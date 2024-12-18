namespace StarshipGame;

public class BridgeCommand : Hwdtech.ICommand, ICommandInjectable
{
    private Hwdtech.ICommand cmd;

    public void Execute()
    {
        cmd.Execute();
    }

    public void Inject(Hwdtech.ICommand command)
    {
        cmd = command;
    }
}
