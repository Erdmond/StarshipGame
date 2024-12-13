namespace StarshipGame;

public interface ICommandReceiver
{
    public void Receive(ICommand cmd);
}
