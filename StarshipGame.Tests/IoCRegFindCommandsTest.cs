using System.Reflection;
using Hwdtech.Ioc;
using Moq;

namespace StarshipGame.Tests;

public class IoCRegFindCommandsTest
{
    interface ITest
    {
    }

    class MyTestCommand : ICommand
    {
        public MyTestCommand(ITest test)
        {
        }

        public void Execute()
        {
            // nothing to do...
        }
    }

    public IoCRegFindCommandsTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        new RegisterIoCDependencyFindCommands().Execute();
    }

    [Fact]
    public void EmptyOnNoAssembly()
    {
        var types = IoC.Resolve<List<Type>>("Commands.FindCommands");
        Assert.Empty(types);
    }

    [Fact]
    public void ReturnTypesOfAssembly()
    {
        Mock<Assembly> assemblyMock = new();
        assemblyMock.Setup(a => a.GetTypes()).Returns([typeof(MyTestCommand)]);

        var types = IoC.Resolve<List<Type>>("Commands.FindCommands", assemblyMock.Object);

        Assert.NotEmpty(types);
        Assert.Equal(types, [typeof(MyTestCommand)]);
    }

    [Fact]
    public void NullThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => IoC.Resolve<List<Type>>("Commands.FindCommands", null));
    }
}
