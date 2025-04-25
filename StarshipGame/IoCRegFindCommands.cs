using System.Reflection;

namespace StarshipGame;
using Hwdtech;
using System.Reflection;

public class RegisterIoCDependencyFindCommands : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
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
