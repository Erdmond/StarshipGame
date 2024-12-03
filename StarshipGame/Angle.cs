namespace StarshipGame;

public class Angle
{
    public int X { get; set; }

    public Angle(int x)
    {
        X = x % 360;
    }

    public static Angle operator +(Angle y1, Angle y2)
    {
        return new Angle(y1.X + y2.X);
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Angle Angle)
        {
            return false;
        }

        return X == Angle.X;
    }

    public override int GetHashCode()
        {
            return HashCode.Combine(X);
        }
}
