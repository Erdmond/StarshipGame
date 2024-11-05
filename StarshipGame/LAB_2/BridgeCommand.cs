namespace StarshipGame;

public class BridgeCommand : ICommand, IInjectable
{
    private ICommand _cmd;

    public void Execute()
    {
        _cmd.Execute();
    }

    public void Inject(ICommand cmd)
    {
        _cmd = cmd;
    }
}