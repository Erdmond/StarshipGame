namespace StarshipGame.Tests;

using Moq;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;
using System.Reflection;

public class RegisterIoCDependencyFactoryRegTests
{
    private readonly List<string> _generatedAdapterStrings = new();
    private readonly Mock<ICommand> _mockActivatedCommand = new();
    private readonly Mock<ICommand> _mockFactoryCommand = new();

    private interface IDummyInterface
    {
        string Property1 { get; set; }
        int Property2 { get; set; }
    }

    public RegisterIoCDependencyFactoryRegTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var newScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", newScope).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Adapter.IsComputed", (object[] args) => (object)true).Execute();
        IoC.Resolve<ICommand>("IoC.Register", "Adapter.ComputeMethod", (object[] args) => (object)"computeMethod").Execute();
        
        IoC.Resolve<ICommand>("IoC.Register", "Adapters.CreateAdapter", (object[] args) =>
        {
            string adapterString = $"{args[0]}_adapter";
            _generatedAdapterStrings.Add(adapterString);
            return (object)adapterString;
        }).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.ActivateCommand", (object[] args) =>
        {
            return (object)_mockActivatedCommand.Object;
        }).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Factories.Create", (object[] args) =>
        {
            return (object)_mockFactoryCommand.Object;
        }).Execute();

        new RegisterIoCDependencyFactoryReg().Execute();
    }

    [Fact]
    public void FactoryRegGeneratesAdapterAndActivatesCommand()
    {
        var interfaces = new List<Type> { typeof(IDummyInterface) };
        IoC.Resolve<object>("FactoriesRegister", interfaces);

        Assert.Single(_generatedAdapterStrings);
        Assert.Contains("IDummyInterface_adapter", _generatedAdapterStrings[0]);

        _mockActivatedCommand.Verify(c => c.Execute(), Times.Once);
        _mockFactoryCommand.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void FactoryRegDoesNotGenerateAdapterWhenNoInterfaces()
    {
        var interfaces = new List<Type>();
        IoC.Resolve<object>("FactoriesRegister", interfaces);

        Assert.Empty(_generatedAdapterStrings);

        _mockActivatedCommand.Verify(c => c.Execute(), Times.Never);
        _mockFactoryCommand.Verify(c => c.Execute(), Times.Never);
    }
}
