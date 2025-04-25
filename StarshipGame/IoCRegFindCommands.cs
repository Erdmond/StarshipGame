using System.Reflection;

namespace StarshipGame;
using Hwdtech;

public class IoCRegFindCommands: ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Commands.FindCommands",
            (object[] args) => 
            {
                var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => typeof(ICommand).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                    .Where(t => t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .All(cn => cn.GetParameters().Length == 1 && cn.GetParameters()[0].ParameterType.IsInterface))
                    .ToList();
                
                return commandTypes;
            }
        ).Execute();
    }
}
