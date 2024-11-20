using Hwdtech;

namespace StarshipGame;

public class CmdStartCommand : Hwdtech.ICommand
{
    private IOrder _Order { get; }
    private Hwdtech.ICommand _Cmd { get; }

    public CmdStartCommand(IOrder order, Hwdtech.ICommand cmd)
    {
        _Order = order;
        _Cmd = cmd;
    }

    public void Execute()
    {
        _Order.InitialValues.ToList().ForEach(item => IoC.Resolve<Hwdtech.ICommand>("InitialValues.Set", _Order.Target, item.Key, item.Value).Execute());

        IoC.Resolve<Hwdtech.ICommand>("InitialValues.Set", _Order.Target, _Cmd.GetType().Name, _Cmd).Execute();

        IoC.Resolve<IQueue>("GQueue").Put(IoC.Resolve<Hwdtech.ICommand>("Command.Set", _Order.Target, _Cmd.GetType().Name));
    }
}
