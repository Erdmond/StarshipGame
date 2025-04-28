using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace StarshipGame;

public static class AdapterActivator
{
    private static readonly Dictionary<string, Type> _typeCache = new();

    public static object Create(Type interfaceType, IDictionary<object, object> data)
    {
        var method = typeof(AdapterActivator)
            .GetMethod("Create", 1, new[] { typeof(IDictionary<object, object>) })!
            .MakeGenericMethod(interfaceType);

        return method.Invoke(null, new object[] { data })!;
    }

    public static T Create<T>(IDictionary<object, object> data) where T : class
    {
        var interfaceType = typeof(T);
        var adapterName = $"{interfaceType.Name.Substring(1)}Adapter";

        if (!_typeCache.TryGetValue(adapterName, out var adapterType))
        {
            var fields = GetFieldsFromInterface<T>();
            var generatedCode = IoC.Resolve<string>(
                "Adapters.CreateAdapter",
                interfaceType.Name,
                fields
            );

            var fullCode = $@"
                using System.Collections.Generic;
                {generatedCode}
            ";

            adapterType = CompileType(fullCode, adapterName);
            _typeCache[adapterName] = adapterType;
        }

        return (T)Activator.CreateInstance(adapterType, data);
    }

    private static Type CompileType(string code, string className)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code);

        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IDictionary<,>).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IoC).Assembly.Location)
        };

        var compilation = CSharpCompilation.Create(
            "DynamicAdapters",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        if (!result.Success)
        {
            var errors = string.Join("\n", result.Diagnostics);
            throw new InvalidOperationException($"Compilation failed:\n{errors}");
        }

        var assembly = Assembly.Load(ms.ToArray());
        return assembly.GetType(className);
    }

    private static Field[] GetFieldsFromInterface<T>()
    {
        return typeof(T).GetProperties()
            .Select(p => new Field(
                name: p.Name,
                type: GetTypeName(p.PropertyType),
                isDefaultGetter: true,
                needSetter: p.CanWrite,
                customGetter: "",
                customSetter: "",
                isDefaultSetter: true))
            .ToArray();
    }

    private static string GetTypeName(Type type)
    {
        if (type.IsGenericType)
        {
            var genericArgs = string.Join(",", type.GetGenericArguments().Select(GetTypeName));
            return $"{type.Name.Split('`')[0]}<{genericArgs}>";
        }
        return type.Name;
    }
}
