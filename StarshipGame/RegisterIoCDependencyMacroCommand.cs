using Hwdtech;

namespace StarshipGame;

public class RegisterIoCDependencyMacroCommand: Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.GetICommandsFromArgs",
            (object[] args) => args.ToList().Select(c => (Hwdtech.ICommand)c).ToArray()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Macro", 
            (object[] args) => new MacroCommand(IoC.Resolve<Hwdtech.ICommand[]>("Commands.GetICommandsFromArgs", args))).Execute();
    }
}
