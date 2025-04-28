using Scriban;

namespace StarshipGame;

public class IoCRegMakeAdapterCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Adapters.MakeField", (object[] args) =>
                new Field((string)args[0], (string)args[1], (bool)args[2],
                    (bool)args[3], (string)args[4], (string)args[5],
                    (bool)args[6]))
            .Execute();

        // interface, fields[]
        IoC.Resolve<ICommand>("IoC.Register", "Adapters.CreateAdapter", (object[] args) =>
        {
            string interfaceName = (string)args[0];
            Field[] fields = (Field[])args[1];

            string[] fieldValues = fields
                .Select(field =>
                {
                    string getter = field.IsDefaultGetter
                        ? Getter(field.Type, field.Name)
                        : CustomGetter(field.CustomGetter);
                    
                    string setter;
                    if (field.NeedSetter)
                        setter = field.IsDefaultSetter
                            ? Setter(field.Type, field.Name)
                            : CustomSetter(field.CustomSetter);
                    else setter = "";

                    return Template.ParseLiquid("public {{type}} {{name}} { {{getter}} {{setter}} }")
                        .Render(new { Type = field.Type, Name = field.Name, Getter = getter, Setter = setter });
                }).ToArray();

            string fieldsInString = string.Join(" ", fieldValues);

            return Template.ParseLiquid("class {{name}}Adapter: {{interfaceName}} { IDictionary<object, object> startObject; public {{name}}Adapter(IDictionary<object, object> _startObject) { startObject = _startObject; }  {{fields}} }")
                .Render(new
                {
                    Name = interfaceName.Substring(1), IntefaceName = interfaceName, Fields = fieldsInString
                });
        }).Execute();
    }

    public string Getter(string type, string name)
        => Template.ParseLiquid("get => ({{type}}) startObject[\"{{name}}\"];")
            .Render(new { Type = type, Name = name });

    public string Setter(string type, string name)
        => Template.ParseLiquid("set => startObject[\"{{name}}\"] = ({{type}})value;")
            .Render(new { Type = type, Name = name });

    public string CustomGetter(string customGetter)
        => Template.ParseLiquid("get => ({{type}}) {{customGetter}}(startObject);")
            .Render(new { CustomGetter = customGetter });

    public string CustomSetter(string customSetter)
        => Template.ParseLiquid("set => {{customSetter}}(startObject, value);")
            .Render(new { CustomSetter = customSetter });
}