namespace StarshipGame;

public interface IObject
{
    object GetProperty(string key);
    void SetProperty(string key, object value);
}
