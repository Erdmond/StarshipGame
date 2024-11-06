namespace StarshipGame;

public class CmdStartCommand : ICommand
{
    private readonly ICommand _cmd { get; };
    private readonly IStartable _order { get; };

    public CmdStartCommand(ICommand cmd, IStartable order)
    {
        _cmd = cmd;
        _order = order;
    }

    public void Execute()
    {
        _order.Recieve(_cmd);
    }
}
