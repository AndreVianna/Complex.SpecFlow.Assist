namespace Complex.SpecFlow.Assist;

internal static class Deserializer {
    private const RegexOptions _regexOptions = Compiled | IgnoreCase | Singleline | IgnorePatternWhitespace;
    private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(10);

    internal static T DeserializeVertical<T>(Table table, IDictionary<string, object> context) {
        return Deserialize<T>(CreateFromVertical(table, context));
    }

    internal static IEnumerable<T> DeserializeHorizontal<T>(Table table, IDictionary<string, object> context) {
        return CreateFromHorizontal(table, context).Select(Deserialize<T>);
    }

    private const string _pathPattern = @"Path:\s\$\.([^\s|]*)"; // Path: $.Id => $1:Id
    private static readonly Regex _pathCapture = new(_pathPattern, _regexOptions, _regexTimeout);

    private static T Deserialize<T>(PropertyCollection properties, int line = -1) {
        try {
            var root = CreateObject(properties);
            return root.Deserialize<T>(new JsonSerializerOptions { IncludeFields = true })!;
        }
        catch (JsonException e) {
            var path = _pathCapture.Match(e.Message).Groups[1].Value;
            var invalidCastException = new InvalidCastException($"The value at '{path}' is not of the correct type.", e);
            if (line == -1) throw invalidCastException;
            throw new InvalidOperationException($"An error has occurred while deserializing line {line}.", invalidCastException);
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
            var value = CreateProperty(objectNode, property!, properties);
            if (property!.Name.ToLower() == "{self}") return value!;
            objectNode[property.Name] = value;
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
            _ when text.StartsWith('{') => GetFromContext(property, text.TrimStart('{').TrimEnd('}'), false),
            _ when text.StartsWith('[') => GetFromContext(property, text.TrimStart('[').TrimEnd(']'), true),
            _ when int.TryParse(text, out var value) => JsonValue.Create(value),
            _ when decimal.TryParse(text, out var value) => JsonValue.Create(value),
            _ => JsonValue.Create(text),
        };
        return GetValueOrArray(parent[property.Name], valueNode, property.Indexes);
    }

    private static JsonNode GetFromContext(Property property, string key, bool asArray) {
        var values = key.Split(':');
        var contextKey = values[0];
        var indexes = Array.Empty<string>();
        if (values.Length > 1) indexes = values[1].Split(',').Select(i => i.Trim()).ToArray();
        return !property.Context.TryGetValue(contextKey, out var value)
            ? throw new InvalidDataException($"The key '{contextKey}' was not found in the deserialization context at '{property.Line.Key}'.")
            : asArray
                ? GetFromContextAsArray(property.Line.Key, contextKey, value, indexes)
                : GetFromContextAsInstance(property.Line.Key, contextKey, value, indexes);
    }

    private static JsonNode GetFromContextAsInstance(string line, string contextKey, object value, IReadOnlyList<string> indexes) {
        var collection = (value as IEnumerable)?.Cast<object>().ToArray() ?? Array.Empty<object>();
        return indexes.Count switch {
            > 1 => throw new InvalidDataException($"An instance value can not have more than one index at '{line}'."),
            > 0 when collection.Length == 0 => throw new InvalidDataException($"The key '{contextKey}' of the deserialization context contains a single object and can't be used with an index at '{line}'."),
            > 0 when int.TryParse(indexes[0], out var index) && index >= 0 && index < collection.Length => SerializeToNode(collection[index], new JsonSerializerOptions { IncludeFields = true })!,
            > 0 => throw new InvalidDataException($"'{indexes[0]}' is not a valid index for the collection contained in the deserialization context under '{contextKey}' at '{line}'."),
            _ when collection.Length > 0 => throw new InvalidDataException($"The key '{contextKey}' of the deserialization context contains a collection and can't be assigned to an instance without an index at '{line}'."),
            _ => SerializeToNode(value)!
        };
    }

    private static JsonNode GetFromContextAsArray(string line, string contextKey, object value, IReadOnlyCollection<string> values) {
        var collection = (value as IEnumerable)?.Cast<object>().ToArray() ?? Array.Empty<object>();
        return values.Count switch {
            > 0 when collection.Length == 0 => throw new InvalidDataException($"The key '{contextKey}' of the deserialization context does not contain a collection at '{line}'."),
            > 0 when TryGetIndexes(values, out var indexes) && indexes.All(index => index >= 0 && index < collection.Length)
                => SerializeToNode(indexes.Select(index => collection[index]).ToArray(), new JsonSerializerOptions { IncludeFields = true })!,
            > 0 => throw new InvalidDataException($"'{string.Join(',', values)}' is not a valid set of indexes for the collection contained in the deserialization context under '{contextKey}' at '{line}'."),
            _ when collection.Length > 0 => SerializeToNode(collection.ToArray(), new JsonSerializerOptions { IncludeFields = true })!,
            _ => SerializeToNode(new[] { value })!
        };
    }

    private static bool TryGetIndexes(IEnumerable<string> values, out int[] indexes) {
        indexes = Array.Empty<int>();
        var result = new List<int>();
        foreach (var value in values) {
            if (!int.TryParse(value, out var index)) return false;
            result.Add(index);
        }
        indexes = result.ToArray();
        return true;
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