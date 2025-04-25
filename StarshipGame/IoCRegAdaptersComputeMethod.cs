namespace StarshipGame;

public class IoCRegAdapterComputeMethod : ICommand
{
    public Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Adapter.ComputeMethod",
            (object[] args) =>
            {
                var fieldName = (string)args[0];
                var isGet = (bool)args[1];
                
                var attributeMethods = IoC.Resolve<Dictionary<(string, bool), string>>("Commands.ParseAttributes");
                return attributeMethods[(fieldName, isGet)];
            }
        ).Execute();
    }
}
