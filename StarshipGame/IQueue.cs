namespace StarshipGame;
using Hwdtech;

public interface IQueue
{
    void Put(Hwdtech.ICommand cmd);
    Hwdtech.ICommand Take();
}
