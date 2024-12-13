namespace StarshipGame;
using Hwdtech;

public class SendCommand : Hwdtech.ICommand
{
    private Hwdtech.ICommand _Cmd;
    private ICommandReceiver _Reciever;

    public SendCommand(Hwdtech.ICommand cmd, ICommandReceiver reciever)
    {
        _Cmd = cmd;
        _Reciever = reciever;
    }

    public void Execute()
    {
        _Reciever.Receive(_Cmd);
    }
}
