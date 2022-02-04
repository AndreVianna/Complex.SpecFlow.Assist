namespace Complex.SpecFlow.Assist;

internal static class Deserializer {
    internal static T Deserialize<T>(Table table) {
        var root = CreateObject(PropertyCollection.Create(table));
        return root.Deserialize<T>(new JsonSerializerOptions { IncludeFields = true })!;
    }

    private static JsonNode CreateObject(PropertyCollection properties) {
        var objectNode = new JsonObject();
        foreach (var property in properties) {
            objectNode[property.Name] = CreateProperty(objectNode, property, properties);
        }

        return objectNode;
    }

    private static JsonNode? CreateProperty(JsonNode parent, Property property, PropertyCollection context)
        => property.Children.Any()
            ? CreateComplexProperty(parent, property, context)
            : CreateSimpleProperty(parent, property);

    private static JsonNode? CreateComplexProperty(JsonNode parent, Property property, PropertyCollection context) {
        var objectNode = CreateObject(context.LevelUp());
        var result = GetValueOrArray(parent[property.Name], objectNode, property.Indexes);
        context.DoNotMoveNext();
        return result;
    }

    private static JsonNode? CreateSimpleProperty(JsonNode parent, Property property) {
        var text = property.Line.Value;
        var valueNode = text switch {
            _ when string.IsNullOrWhiteSpace(text) => default,
            _ when text.ToLower() is "null" or "default" => default,
            _ when bool.TryParse(text, out var value) => JsonValue.Create(value),
            _ when int.TryParse(text, out var value) => JsonValue.Create(value),
            _ when double.TryParse(text, out var value) => JsonValue.Create(value),
            _ when text.StartsWith('"') => JsonValue.Create(text.Trim('"')),
            _ => throw new InvalidCastException($"Invalid value at '{property.Line.Key}'."),
        };
        return GetValueOrArray(parent[property.Name], valueNode, property.Indexes);
    }

    private static JsonNode? GetValueOrArray(JsonNode? propertyNode, JsonNode? valueNode, int[] indexes) {
        return indexes.Any()
            ? CreateOrUpdateArray(propertyNode, valueNode, indexes)
            : valueNode;
    }

    private static JsonNode CreateOrUpdateArray(JsonNode? propertyNode, JsonNode? valueNode, int[] indexes) {
        var arrayNode = propertyNode?.AsArray() ?? new JsonArray();
        var index = indexes.First();
        var value = GetValueOrArray(index < arrayNode.Count ? arrayNode[index] : null, valueNode, indexes.Skip(1).ToArray());
        if (index == arrayNode.Count) arrayNode.Add(value);
        return arrayNode;
    }
}