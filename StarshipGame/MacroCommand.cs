using System.Linq;

namespace StarshipGame;

public class MacroCommand : Hwdtech.ICommand
{
    private Hwdtech.ICommand[] cmds;

    public MacroCommand(Hwdtech.ICommand[] cmds)
    {
        this.cmds = cmds;
    }

    public void Execute()
    {
        cmds.ToList().ForEach(c => c.Execute());
    }
}
