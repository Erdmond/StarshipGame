using Xunit;
using Hwdtech;

namespace StarshipGame.Tests;

public class ActivateCommandTests
{
    [Fact]
    public void Execute_CompilesAndExecutesCommandSuccessfully()
    {
        var code = @"
            using StarshipGame;
            using Hwdtech;

            public class TestCommand : Hwdtech.ICommand
            {
                public static bool WasExecuted = false;
                public void Execute() { WasExecuted = true; }
            }";

        var command = new ActivateCommand(code);
        command.Execute();

        var testCommandType = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == "TestCommand");

        Assert.NotNull(testCommandType);

        var wasExecutedField = testCommandType?.GetField("WasExecuted");
        Assert.NotNull(wasExecutedField);

        var wasExecuted = (bool)(wasExecutedField?.GetValue(null) ?? false);
        Assert.True(wasExecuted);
    }
}
