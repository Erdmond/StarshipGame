namespace StarshipGame;

using Hwdtech;
using System.Reflection;

public class IoCRegActivateAdaptersCommand : ICommand
{
    public void Execute()
    {
        var createMethod = typeof(AdapterActivator).GetMethod("Create");

        var adapterDictionary = IoC.Resolve<IEnumerable<Type>>("Commands.FindCommands")
                .Select(cmd => cmd.GetConstructors()[0].GetParameters()[0].ParameterType)
                .Distinct()
                .ToDictionary(
                    i => i,
                    i => createMethod.MakeGenericMethod(i)
                        .Invoke(null, new object[] { new Dictionary<object, object>() }));

        IoC.Resolve<ICommand>("IoC.Register", "Adapters.ActivatedDictionary", (object[] args) => adapterDictionary).Execute();
    }
}
