namespace StarshipGame;

public class IoCRegAdapterIsComputed : ICommand
{
    public Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Adapter.IsComputed",
            (object[] args) => 
            {
                var paramName = (string)args[0];
                var interfaceName = (string)args[1];

                // по именам определяю вычислимо ли paramName в interfaceName

                var isComputed = true; // bool значение определения

                return isComputed;
            }
        ).Execute();
    }
}
