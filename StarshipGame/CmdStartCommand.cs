using Hwdtech;

namespace StarshipGame;

public class CmdStartCommand : Hwdtech.ICommand
{
    private IOrder _order { get; }
    private Hwdtech.ICommand _cmd { get; }

    public CmdStartCommand(IOrder order, Hwdtech.ICommand cmd)
    {
        _order = order;
        _cmd = cmd;
    }

    public void Execute()
    {
        _order.InitialValues.ToList().ForEach(item => IoC.Resolve<Hwdtech.ICommand>("InitialValues.Set", _order.Target, item.Key, item.Value).Execute());

        IoC.Resolve<Hwdtech.ICommand>("InitialValues.Set", _order.Target, "Command", _cmd).Execute();

        IoC.Resolve<IQueue>("GQueue").Put(_cmd);
    }
}
