using System.Reflection;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests;

public class IoCRegCollectInterfacesTest
{
    interface ITestInterface1 { }
    interface ITestInterface2 { }

    class TestCommand1 : ICommand
    {
        public TestCommand1(ITestInterface1 test) { }
        public void Execute() { }
    }

    class TestCommand2 : ICommand
    {
        public TestCommand2(ITestInterface2 test) { }
        public void Execute() { }
    }

    class TestCommandWithSameInterface : ICommand
    {
        public TestCommandWithSameInterface(ITestInterface1 test) { }
        public void Execute() { }
    }

    public IoCRegCollectInterfacesTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        new IoCRegCollectInterfaces().Execute();
    }

    [Fact]
    public void EmptyOnNoCommands()
    {
        var interfaces = IoC.Resolve<List<Type>>("Commands.CollectInterfaces");
        Assert.Empty(interfaces);
    }

    [Fact]
    public void ReturnsInterfacesFromCommands()
    {
        var commandTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommand2) };

        var interfaces = IoC.Resolve<List<Type>>("Commands.CollectInterfaces", commandTypes);

        Assert.Equal(2, interfaces.Count);
        Assert.Contains(typeof(ITestInterface1), interfaces);
        Assert.Contains(typeof(ITestInterface2), interfaces);
    }

    [Fact]
    public void ReturnsDistinctInterfaces()
    {
        var commandTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommandWithSameInterface) };

        var interfaces = IoC.Resolve<List<Type>>("Commands.CollectInterfaces", commandTypes);

        Assert.Single(interfaces);
        Assert.Contains(typeof(ITestInterface1), interfaces);
    }

    [Fact]
    public void NullThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            IoC.Resolve<List<Type>>("Commands.CollectInterfaces", null));
    }
}
