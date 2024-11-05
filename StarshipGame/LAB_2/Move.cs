namespace StarshipGame;

public class MoveCommand : ICommand
{
    private IMovable obj;

    public MoveCommand(IMovable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.Position = obj.Position.Select((value, index) => value + obj.Velocity[index]).ToArray();
    }
}