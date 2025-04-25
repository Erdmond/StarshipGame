namespace StarshipGame;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CustomMethodAttribute : Attribute
{
    public string Name { get; }

    public CustomMethodAttribute(string name)
    {
        Name = name;
    }
}
