namespace StarshipGame;

public class GenerateAdaptersCommand : ICommand
{
    // какие-то поля

    public GenerateAdaptersCommand()
    {
        // какой-то конструктор
    }

    public void Execute()
    {
        var commandTypes = IoC.Resolve<List<Type>>("Commands.FindCommands");

        // далее обработка этих типов, генерация адаптеров на их основе
    }
}
