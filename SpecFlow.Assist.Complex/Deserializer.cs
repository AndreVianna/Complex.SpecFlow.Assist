namespace SpecFlow.Assist.Complex;

internal static class Deserializer {
    internal static T? Deserialize<T>(Table table) {
        var result = GetJsonObject(table.Rows.GetEnumerator());
        return result.Deserialize<T>(new JsonSerializerOptions { IncludeFields = true });
    }

    private static JsonNode GetJsonObject(IEnumerator<TableRow?> enumerator, int level = 0) {
        return CreateObject(new Context(enumerator, level));
    }

    private static JsonNode CreateObject(Context context) {
        var objectNode = new JsonObject();
        while (context.HasMore) {
            AddPropertyTo(context, objectNode);
        }

        return objectNode;
    }

    private static void AddPropertyTo(Context context, JsonNode targetNode) {
        var property = context.Current;
        if (property!.Children.Any()) {
            AddComplexProperty(context, targetNode, property);
            context.UpdateCurrent();
        }
        else {
            AddSimpleProperty(context, targetNode, property);
            context.MoveNext();
        }
    }

    private static void AddComplexProperty(Context context, JsonNode targetNode, Property property) {
        var objectNode = GetJsonObject(context.Enumerator, context.Level + 1);
        targetNode[property.Name] = GetValueOrArrayNode(context, targetNode[property.Name], objectNode, property.Indexes);
    }

    private static void AddSimpleProperty(Context context, JsonNode targetNode, Property property) {
        var text = context.Current!.Value;
        var valueNode = text switch {
            _ when string.IsNullOrWhiteSpace(text) => default,
            _ when text.ToLower() is "null" or "default" => default,
            _ when bool.TryParse(text, out var value) => JsonValue.Create(value),
            _ when int.TryParse(text, out var value) => JsonValue.Create(value),
            _ when double.TryParse(text, out var value) => JsonValue.Create(value),
            _ when text.StartsWith('"') => JsonValue.Create(text.Trim('"')),
            _ => throw new InvalidCastException($"Invalid value at '{context.Current.Key}'."),
        };
        targetNode[property.Name] = GetValueOrArrayNode(context, targetNode[property.Name], valueNode, property.Indexes);
    }

    private static JsonNode? GetValueOrArrayNode(Context context, JsonNode? targetNode, JsonNode? node, int[] indexes) {
        return indexes.Any()
            ? GetArrayNode(context, targetNode, node, indexes)
            : node;
    }

    private static JsonNode GetArrayNode(Context context, JsonNode? targetNode, JsonNode? valueNode, int[] indexes) {
        var array = targetNode?.AsArray() ?? new JsonArray();
        var index = indexes.First();
        var value = GetValueOrArrayNode(context, index < array.Count ? array[index] : null, valueNode, indexes.Skip(1).ToArray());
        if (index == array.Count) array.Add(value);
        return array;
    }
}