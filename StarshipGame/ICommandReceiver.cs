namespace StarshipGame;
using Hwdtech;

public interface ICommandReceiver
{
    public void Receive(Hwdtech.ICommand cmd);
}
