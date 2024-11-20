namespace StarshipGame;

public class Vector
{
    private int[] data;

    public Vector(int[] data)
    {
        this.data = data;
    }

    public static Vector operator +(Vector a, Vector b)
    {
        var res = new Vector(a.data.Select((e, i) => e + b.data[i]).ToArray());
        return res;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Vector other)
        {
            return data.SequenceEqual(other.data);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return data.Aggregate(17, (current, item) => current * 31 + item);
    }
}
