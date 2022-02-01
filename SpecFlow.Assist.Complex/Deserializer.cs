namespace SpecFlow.Assist.Complex;

internal static class Deserializer {
    internal static T? Deserialize<T>(IDictionary<string, string> source) {
        var context = new DeserializationContext(source);
        var result = ProcessSource(context);
        return result.Deserialize<T>();
    }

    private static JsonNode ProcessSource(DeserializationContext context, string? parentPath = null) {
        var result = new JsonObject();
        bool finished;
        do {
            (context.CurrentKey, context.CurrentValue) = context.Enumerator.Current;
            context.ParentPath = parentPath ?? context.ParentPath;
            if (!context.CurrentKey.StartsWith(context.ParentPath)) return result;
            finished = ProcessCurrentProperty(context, result);
        } while (!finished);
        return result;
    }

    private static bool ProcessCurrentProperty(DeserializationContext context, JsonObject result)
        => !context.HasChildren
            ? AddSimpleProperty(context, result)
            : AddComplexProperty(context, result);

    private static bool AddComplexProperty(DeserializationContext context, JsonObject result) {
        var parentPath = context.ParentPath;
        var nodeName = context.Name;
        var isArray = context.IsArray;
        var nodeValue = ProcessSource(context, $"{context.ParentPath}{context.CurrentToken}.");
        AddValueOrArray(result, nodeName, nodeValue, isArray);
        context.ParentPath = parentPath;
        return false;
    }

    private static bool AddSimpleProperty(DeserializationContext context, JsonObject result) {
        var nodeValue = GetNodeTypedValue(context);
        AddValueOrArray(result, context.Name, nodeValue, context.IsArray);
        return !context.Enumerator.MoveNext();
    }

    private static JsonNode? GetNodeTypedValue(DeserializationContext context) =>
        context.CurrentValue switch {
            "Null" or "null" or "NULL" => default,
            "True" or "true" or "TRUE" => JsonValue.Create(true),
            "False" or "false" or "FALSE" => JsonValue.Create(false),
            _ when string.IsNullOrWhiteSpace(context.CurrentValue) => default,
            _ when context.CurrentValue.StartsWith("\"") => JsonValue.Create(context.CurrentValue.Remove(context.CurrentValue.Length - 1).Remove(0, 1)),
            _ when int.TryParse(context.CurrentValue, out var number) => JsonValue.Create(number),
            _ when double.TryParse(context.CurrentValue, out var number) => JsonValue.Create(number),
            _ => throw new InvalidCastException(),
        };

    private static void AddValueOrArray(JsonObject result, string name, JsonNode? nodeValue, bool isArray) {
        if (isArray) {
            var array = result[name]?.AsArray() ?? new JsonArray();
            result.Remove(name);
            array.Add(nodeValue);
            nodeValue = array;
        }
        result.Add(name, nodeValue);
    }
}