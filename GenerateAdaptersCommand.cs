namespace StarshipGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class GenerateAdaptersCommand : ICommand
{
    // какие-то поля

    public GenerateAdaptersCommand()
    {
        // какой-то конструктор
    }

    public void Execute()
    {
        var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly =>
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch
                {
                    return Enumerable.Empty<Type>();
                }
            })
            .Where(type => typeof(ICommand).IsAssignableFrom(type) 
                            && !type.IsAbstract 
                            && !type.IsInterface);

        var validCommands = new List<Type>();

        foreach (var type in commandTypes)
        {
            var constructors = type.GetConstructors(
                BindingFlags.Instance | 
                BindingFlags.Public | 
                BindingFlags.NonPublic
                );

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();

                if (parameters.Length == 1 && parameters[0].ParameterType.IsInterface)
                {
                    validCommands.Add(type);
                    break;
                }
            }
        }
    }
}
