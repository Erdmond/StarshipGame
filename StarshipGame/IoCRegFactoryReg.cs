using System.Reflection;

namespace StarshipGame;

using Hwdtech;

public class RegisterIoCDependencyFactoryReg : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
            "IoC.Register",
            "FactoriesRegister",
            (object[] args) =>
            {
                List<Type> interfaces = (List<Type>)args[0];

                string[] adapterStrings = interfaces.Select(i => IoC.Resolve<string>("Adapters.CreateAdapter", i.Name,
                    i.GetProperties()
                        .Select(pi =>
                            {
                                var defaultGetter = IoC.Resolve<bool>("Adapter.IsComputed", pi.Name, true);
                                var defaultSetter = IoC.Resolve<bool>("Adapter.IsComputed", pi.Name, false);

                                return new Field(
                                    pi.GetType().Name,
                                    pi.Name,
                                    defaultGetter,
                                    defaultSetter,
                                    defaultGetter ? null : IoC.Resolve<string>("Adapter.ComputeMethod", pi.Name, true),
                                    defaultSetter ? null : IoC.Resolve<string>("Adapter.ComputeMethod", pi.Name, false),
                                    pi.CanWrite
                                );
                            }
                        ))).ToArray();

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