namespace StarshipGame;
public class RunnableCommand : ICommand
{
    private readonly Action _action;
    public RunnableCommand(Action action)
    {
        _action = action;
    }

    public void Execute()
    {
        _action();
    }
}
