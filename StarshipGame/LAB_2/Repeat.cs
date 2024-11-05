namespace StarshipGame;

public class RepeatCommand : ICommand
{
    private ICommand _command;
    private ICommandReciever _reciever;

    public RepeatCommand(ICommand command, ICommandReciever reciever)
    {
        _command = command;
        _reciever = reciever;
    }

    public void Execute()
    {
        _reciever.Enqueue(_command);
    }
}