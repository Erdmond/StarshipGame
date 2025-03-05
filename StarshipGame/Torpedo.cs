namespace StarshipGame;
public class Torpedo : IMovable
{
    public Vector Position { get; set; }
    public Vector Velocity { get; private set; }

    public Torpedo(Vector initialPosition, Vector initialVelocity)
    {
        Position = initialPosition;
        Velocity = initialVelocity;
    }
}
