namespace StarshipGame;

public class ShootCommand : ICommand
{
    private readonly IObjectInfo _objectInfo;

    public ShootCommand(IObjectInfo objectInfo) => _objectInfo = objectInfo;

    public void Execute()
    {
        ICommand authCommand = IoC.Resolve<ICommand>("Commands.Auth", _objectInfo);
        ICommand createTorpedoCommand = IoC.Resolve<ICommand>("Commands.CreateTorpedo", _objectInfo);
        ICommand macroCommand = IoC.Resolve<ICommand>("Commands.Macro", new object[] { authCommand, createTorpedoCommand });

        IoC.Resolve<ICommand>("Game.EnqueueCommand", macroCommand).Execute();
    }
}
