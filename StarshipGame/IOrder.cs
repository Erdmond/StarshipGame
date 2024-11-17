namespace StarshipGame;

public interface IOrder
{
    IObject Target { get; }
    Dictionary<string, object> InitialValues { get; }
}
