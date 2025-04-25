using System.Reflection;

namespace StarshipGame;
using System.Reflection;

public class IoCRegParseAttributes : ICommand
{
    public void Execute()
    {
        var attributesDictionary = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .SelectMany(type => type.GetMethods(
                BindingFlags.Public | 
                BindingFlags.NonPublic | 
                BindingFlags.Instance | 
                BindingFlags.Static)
                .Select(method => new { Type = type, Method = method }))
            .SelectMany(m => m.Method.GetCustomAttributes(typeof(CustomMethodAttribute))
                .Cast<CustomMethodAttribute>()
                .Select(attr => new { 
                    m.Type, 
                    m.Method, 
                    Attr = attr 
                }))
            .Where(x => x.Attr != null)
            .GroupBy(x => (x.Attr.Name, x.Attr.IsGet))
            .ToDictionary(
                g => g.Key,
                g => $"{g.First().Type.Name}.{g.First().Method.Name}"
            );

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Commands.ParseAttributes",
            (object[] _) => attributesDictionary
        ).Execute();
    }
}
