using Hwdtech.Ioc;

namespace StarshipGame.Tests;

public class DynamicCommandActivatorTests
{
    public DynamicCommandActivatorTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute() => _action();
    }

    [Fact]
    public void Activate_ValidCommandClass_ExecutesSuccessfully()
    {
        bool executed = false;

        IoC.Resolve<ICommand>("IoC.Register", "Test.ExecuteFlag", (object[] _) => new DelegateCommand(() => executed = true)).Execute();

        var code = @"
            using System;
            using StarshipGame;
            using Hwdtech;

            public class SampleCommand : Hwdtech.ICommand
            {
                public void Execute()
                {
                    IoC.Resolve<ICommand>(""Test.ExecuteFlag"").Execute();
                }
            }
        ";

        DynamicCommandActivator.Activate(code);

        Assert.True(executed);
    }

    [Fact]
    public void Activate_InvalidCode_ThrowsCompilationException()
    {
        var invalidCode = "public class {";

        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            DynamicCommandActivator.Activate(invalidCode);
        });

        Assert.Contains("Compilation failed", ex.Message);
    }

    [Fact]
    public void Activate_NoICommandTypes_DoesNotThrowOrExecute()
    {
        var code = @"
            using System;

            public class NonCommandClass
            {
                public void Foo() {}
            }
        ";

        try
        {
            DynamicCommandActivator.Activate(code);
        }
        catch (Exception ex)
        {
            Assert.False(true, $"Expected no exception, but got: {ex.Message}");
        }
    }
}
