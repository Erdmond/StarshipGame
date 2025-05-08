using Hwdtech.Ioc;

namespace StarshipGame.Tests;

public class IoCRegMakeAdapterLineCommandTest
{

    interface ITest
    {
        public int TestInt { get; set; }
        public string TestString { get; set; }
    }

    public class MyClass
    {
        public static string GetTestString(IDictionary<object, object> startObject)
        {
            return "hello";
        }

        public static void SetTestString(IDictionary<object, object> startObject, string val)
        {
            // something did...
        }
    }

    public IoCRegMakeAdapterLineCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        new IoCRegMakeAdapterCommand().Execute();
    }

    [Fact]
    public void MakeAdapterLineProperlyWithAllFieldsFactoryAndIocStrategy()
    {
        string adapter = IoC.Resolve<string>("Adapters.CreateAdapter", "ITest", new[]
        {
            new Field("int", "TestInt", true, true, null, null, true),
            new Field("string", "TestString", false, false, "MyClass.GetTestString", "MyClass.SetTestString", true)
        });

        Assert.Equal(@"class TestAdapter: ITest { 
                IDictionary<object, object> startObject; 
                public TestAdapter(IDictionary<object, object> _startObject) 
                { startObject = _startObject; } 

                public int TestInt { get => (int) startObject[""TestInt""]; set => startObject[""TestInt""] = (int)value; } public string TestString { get => MyClass.GetTestString(startObject); set => MyClass.SetTestString(startObject, value); } 
                }  
                class TestFactory: IFactory, ICommand
                { 
                public object Adapt(IDictionary<object, object> startObject) 
                { return new TestAdapter(startObject); } 

                public void Execute() 
                { IoC.Resolve<ICommand>(""IoC.Register"", ""Adapters.Test"", (object[] args) => this.Adapt((IDictionary<object, object>) args[0])).Execute(); } 
                }", adapter);
    }

    [Fact]
    public void UntitledAdapterThrowsException()
    {
        Assert.Throws<ArgumentException>(() => IoC.Resolve<string>("Adapters.CreateAdapter", "", new Field[]{}));
    }

    [Fact]
    public void OnlyIClassThrowsException()
    {
        Assert.Throws<ArgumentException>(() => IoC.Resolve<string>("Adapters.CreateAdapter", "I", new Field[] { }));
    }

    [Fact]
    public void WithoutFieldsThrowsException()
    {
        Assert.Throws<IndexOutOfRangeException>(() => IoC.Resolve<string>("Adapters.CreateAdapter", "IInterface"));
    }
    
    [Fact]
    public void NullFieldsThrowsException()
    {
        Assert.Throws<ArgumentNullException>(() => IoC.Resolve<string>("Adapters.CreateAdapter", "IInterface", null));
    }

}