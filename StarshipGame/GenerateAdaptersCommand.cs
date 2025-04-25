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
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(ICommand).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
            .Where(t => t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .All(cn => cn.GetParameters().Length == 1 && cn.GetParameters()[0].ParameterType.IsInterface))
            .ToList();
    }
}
