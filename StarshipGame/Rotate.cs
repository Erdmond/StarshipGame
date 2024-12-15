namespace StarshipGame;
using Hwdtech;

public class RotateCommand : Hwdtech.ICommand
{
    private IRotatable Obj;

    public RotateCommand(IRotatable obj)
    {
        this.Obj = obj;
    }

    public void Execute()
    {
        Obj.Angle += Obj.AngleVelocity;
    }
}
