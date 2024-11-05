namespace StarshipGame;

public interface ICommandReciever
{
    public void Enqueue(ICommand command);
}