namespace StarshipGame;

public class Angle
{
    private double CosValue { get; }

    public double Cosine => CosValue;

    public Angle(double degrees)
    {
        CosValue = Math.Cos(degrees * Math.PI / 180);
    }

    private Angle(double cosValue, bool isCosine)
    {
        CosValue = cosValue;
    }

    public static Angle operator +(Angle v1, Angle v2)
    {
        double newDegrees = Math.Acos(v1.CosValue) * 180 / Math.PI + Math.Acos(v2.CosValue) * 180 / Math.PI;
        return new Angle(newDegrees % 360);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Angle newAngle)
        {
            return false;
        }

        return Math.Abs(CosValue - newAngle.CosValue) < 1e-10;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CosValue);
    }
}

