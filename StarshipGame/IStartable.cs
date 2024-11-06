namespace StarshipGame;

public interface IStartable
{
    public void Recieve(ICommand cmd);
}
