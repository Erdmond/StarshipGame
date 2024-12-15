namespace StarshipGame;

public interface IDictionary : ICommand
{
    Dictionary<string, object> Values { get; }
}
