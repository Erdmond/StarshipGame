using Hwdtech;

namespace StarshipGame;

public class CmdEndCommand : Hwdtech.ICommand
{
    private Hwdtech.ICommand _Cmd;
    private IOrder _Obj;
    public CmdEndCommand(Hwdtech.ICommand Cmd, IOrder Obj)
    {
        _Cmd = Cmd;
        _Obj = Obj;
    }

    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("InitialValues.Delete", _Obj.Target, _Cmd.GetType().Name).Execute();

        IoC.Resolve<IInjectable>("Game.Object", _Obj.Target, _Cmd.GetType().Name).Inject(new EmptyCommand());
    }
}

