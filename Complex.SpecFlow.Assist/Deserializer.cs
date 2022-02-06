namespace Complex.SpecFlow.Assist;

internal static class Deserializer {
    internal static T DeserializeVertical<T>(Table table) {
        return Deserialize<T>(CreateFromVertical(table));
    }

    internal static IEnumerable<T> DeserializeHorizontal<T>(Table table) {
        return CreateFromHorizontal(table)
            .Select(Deserialize<T>);
    }

    private static T Deserialize<T>(PropertyCollection properties, int line = -1) {
        try {
            var root = CreateObject(properties);
            return root.Deserialize<T>(new JsonSerializerOptions { IncludeFields = true })!;
        }
        catch (Exception e) {
            if (line == -1) throw;
            throw new InvalidOperationException($"An error has occurred while deserializing line {line}.", e);
        }
        finally {
            properties.Dispose();
        }
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
            _ when text.StartsWith('"') => JsonValue.Create(text.Trim('"')),
            _ when text.StartsWith('\'') => JsonValue.Create(text.Trim('\'')),
            _ when int.TryParse(text, out var value) => JsonValue.Create(value),
            _ when decimal.TryParse(text, out var value) => JsonValue.Create(value),
            _ => JsonValue.Create(text),
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