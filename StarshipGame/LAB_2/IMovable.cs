namespace StarshipGame;

public interface IMovable
{
    public int[] Position { get; set; }
    public int[] Velocity { get; }
}