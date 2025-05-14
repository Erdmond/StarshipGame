namespace StarshipGame.Tests;

using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

public class RegisterIoCDependencyCommandsRegTests
{
    private readonly List<string> _generatedLines = new();
    private readonly Mock<ICommand> _mockActivatedCommand = new();

    private interface IDummyCommand : ICommand { }

    private class DummyCommand : IDummyCommand
    {
        public void Execute() { }
    }

    public RegisterIoCDependencyCommandsRegTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var newScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", newScope).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Adapters.Command.MakeLine", (object[] args) =>
        {
            string line = $"code_for_{args[0]}_{args[1]}";
            _generatedLines.Add(line);
            return line;
        }).Execute();

        _mockActivatedCommand.Setup(c => c.Execute());
        IoC.Resolve<ICommand>("IoC.Register", "Commands.ActivateCommand", (object[] args) =>
        {
            string code = (string)args[0];
            return _mockActivatedCommand.Object;
        }).Execute();

        new RegisterIoCDependencyCommandsReg().Execute();
    }

    [Fact]
    public void CommandsRegisterGeneratesCodeAndActivatesEachCommand()
    {
        var commands = new List<Type> { typeof(DummyCommand) };
        var registerIoC = IoC.Resolve<object>("CommandsRegister", commands);

        Assert.Single(_generatedLines);
        Assert.Contains("DummyCommand", _generatedLines[0]);
        _mockActivatedCommand.Verify(c => c.Execute(), Times.Once);
    }
}
