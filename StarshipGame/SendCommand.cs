namespace StarshipGame;

public class SendCommand : ICommand
{
    private _Cmd;
    private _Reciever;

    public SendCommand(ICommand cmd, ICommandReceiver reciever)
    {
        _Cmd = cmd;
        _Reciever = reciever;
    }

    public void Execute()
    {
        reciever.Receive(cmd);
    }
}
/*
SendCommand в конструктор получает команду ICommand и интерфейс ICommandReceiver.
В методе Execute SendCommand вызывается метод Receive интерфейса ICommandReceiver, в который передается длительная команда.
*/
