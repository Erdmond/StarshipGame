namespace StarshipGame;

public class MultyCommand : ICommand
{
    public Queue<ICommand> PartMulty;

    public MultyCommand(Queue<ICommand> commands)
    {
        PartMulty = commands;
    }

    public void Execute()
    {
        while (PartMulty.Count > 0)
        {
            var command = PartMulty.Dequeue();
            command.Execute();
        }
    }
}
