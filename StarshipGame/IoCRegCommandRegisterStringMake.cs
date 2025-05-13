using Scriban;

namespace StarshipGame;

public class IoCRegCommandRegisterStringMake : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Adapters.Command.MakeLine", (object[] args) =>
        {
            if (((string)args[0]).Length == 0 || args[0] == null)
                throw new Exception("Invalid Command Name");

            if (((string)args[1]).Length == 0 || args[1] == null)
                throw new Exception("Invalid Class Name");

            return Template
                .ParseLiquid(
                    @"IoC.Resolve<ICommand>(""IoC.Register"", ""Commands.{{command}}"", (object[] args) => new {{command}}(IoC.Resolve<I{{cls}}>(""Adapters.{{cls}}"", args[0]))).Execute();")
                .Render(new { command = args[0], cls = args[1] });
        }).Execute();
    }
}