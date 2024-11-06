namespace StarshipGame;

public class BridgeCommand: ICommand, IInjectable
{
    private ICommand cmd;

    public void Execute()
    {
        cmd.Execute();
    }

    public void Inject(ICommand command)
    {
        cmd = command;
    }
}
