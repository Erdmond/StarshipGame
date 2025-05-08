namespace StarshipGame;
using Hwdtech;

public class RegisterIoCDependencyFactoryReg : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "FactoriesRegistrate",
            (object[] args) =>
            {
                List<Type> interfaces = args[0];

                interfaces.Select(i => i.)

                // отправляем каждый интерфейс поочерёдно в ioc:

                // string adapter = IoC.Resolve<string>("Adapters.CreateAdapter", "ITest", new[]
                // {
                //     new Field("int", "TestInt", true, true, null, null, true),
                //     new Field("string", "TestString", false, false, "MyClass.GetTestString", "MyClass.SetTestString", true)
                // });

                // поля получаем из интерфейса
                // полученную строчку как-то активируем
                // из строчки делаем инстанс фабрики и сразу сделать ей execute чтобы зарегистрировать зависимость
            }
        ).Execute();
    }
}
