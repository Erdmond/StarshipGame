namespace StarshipGame;

[AttributeUsage(AttributeTargets.Method)]
public class CustomMethodAttribute(string Name, bool isGet) : Attribute { }
