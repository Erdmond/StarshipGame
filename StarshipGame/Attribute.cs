namespace StarshipGame;

[AttributeUsage(AttributeTargets.Method)]
public class CustomMethodAttribute(string Name, bool isGet) : Attribute { }


public class Extension
{
    [CustomMethod("Velocity", true)]
    public static Vector GetVelocity(IDictionary<object, object> startObject)
    {
        return new Vector(null);
    }
    
    [CustomMethod("Velocity", false)]
    public static void SetVelocity(IDictionary<object, object> startObject, object value)
    {
        //...
    }
    
    // ("Velocity", true) => Extension.GetVelocity
    // ("Velocity", false) => Extension.SetVelocity
}