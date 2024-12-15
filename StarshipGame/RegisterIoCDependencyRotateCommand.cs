namespace StarshipGame;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterIoCDependencyRotateCommand : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "Commands.Rotate",
            (object[] args) => (new RotateCommand(IoC.Resolve<IRotatable>("Adapters.IRotatingObject", args[0])))).Execute();
    }
}
