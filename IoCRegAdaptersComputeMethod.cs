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
                var paramName = (string)args[0];
                var interfaceName = (string)args[1];

                var computeMethod = new Function<>();

                return computeMethod;
            }
        ).Execute();
    }
}