using static Complex.SpecFlow.Assist.Factories.PropertyCollectionFactory;
using static System.Text.Json.JsonSerializer;
using static Complex.SpecFlow.Assist.Collections.PropertyCollection.TableDirection;
using static System.Text.RegularExpressions.RegexOptions;

namespace Complex.SpecFlow.Assist;

internal static class Deserializer {
    private const RegexOptions _regexOptions = Compiled | IgnoreCase | Singleline | IgnorePatternWhitespace;
    private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(10);

    internal static T DeserializeVertical<T>(Table table, IDictionary<string, object> context, Func<T, IDictionary<string, object>, int, IReadOnlyList<T>, IDictionary<string, string?>, T> getUpdatedInstance) {
        return DeserializeInstance(context, CreateFromVertical(table, context), -1, new List<T>(), getUpdatedInstance);
    }

    internal static IEnumerable<T> DeserializeHorizontal<T>(Table table, IDictionary<string, object> context, Func<T, IDictionary<string, object>, int, IReadOnlyList<T>, IDictionary<string, string?>, T> getUpdatedInstance) {
        var previous = new List<T>();
        var index = 0;
        foreach (var instance in CreateFromHorizontal(table, context)) {
            var item = DeserializeInstance(context, instance, index, previous, getUpdatedInstance);
            yield return item;
            previous.Add(item);
            index++;
        }
    }

    private static T DeserializeInstance<T>(IDictionary<string, object> context, PropertyCollection properties, int index, IReadOnlyList<T> previous, Func<T, IDictionary<string, object>, int, IReadOnlyList<T>, IDictionary<string, string?>, T> getUpdatedInstance) {
        var extras = new Dictionary<string, string?>();
        var result = Deserialize(properties, index, previous, extras);
        result = getUpdatedInstance(result, context, index, previous, extras);
        return result;
    }

    private const string _pathPattern = @"Path:\s\$\.([^\s|]*)"; // Path: $.Id => $1:Id
    private static readonly Regex _pathCapture = new(_pathPattern, _regexOptions, _regexTimeout);
    private static T Deserialize<T>(PropertyCollection properties, int index, IReadOnlyList<T> previous, IDictionary<string, string?> extras) {
        try {
            var root = CreateObject(properties, index, previous, extras);
            return root.Deserialize<T>(new JsonSerializerOptions { IncludeFields = true })!;
        }
        catch (JsonException e) {
            var path = _pathCapture.Match(e.Message).Groups[1].Value;
            var invalidCastException = new InvalidCastException($"The value at '{path}' is not of the correct type.", e);
            if (index == -1) throw invalidCastException;
            throw new InvalidOperationException($"An error has occurred while deserializing line {index}.", invalidCastException);
        }
        catch (Exception e) {
            if (index == -1) throw;
            throw new InvalidOperationException($"An error has occurred while deserializing line {index}.", e);
        }
        finally {
            properties.Dispose();
        }
    }

    private static JsonNode CreateObject<T>(PropertyCollection properties, int index, IReadOnlyList<T> previous, IDictionary<string, string?>? extras = null) {
        var objectNode = new JsonObject();
        foreach (var property in properties) {
            if (UpdateExtraValuesOnly(property, extras)) continue;
            var value = CreateProperty(objectNode, property, properties, index, previous);
            if (string.IsNullOrWhiteSpace(property.Name) && properties.Count == 1 && properties.Direction == Horizontal) return value!;
            objectNode[property.Name] = value;
        }

        return objectNode;
    }

    private static bool UpdateExtraValuesOnly(Property property, IDictionary<string, string?>? extras) {
        if (!property.Name.StartsWith('!')) return false;
        if (extras is null) throw new InvalidDataException($"Only top level keys can be assigned to the extra key in the context at '{property.Line.Key}'.");
        var name = property.Name.TrimStart('!');
        extras[name] = property.Line.Value;
        return true;
    }

    private static JsonNode? CreateProperty<T>(JsonNode parent, Property property, PropertyCollection properties, int index, IReadOnlyList<T> previous)
        => property.Children.Any()
            ? CreateComplexProperty(parent, property, properties, index, previous)
            : CreateSimpleProperty(parent, property, index, previous);

    private static JsonNode? CreateComplexProperty<T>(JsonNode parent, Property property, PropertyCollection properties, int index, IReadOnlyList<T> previous) {
        var objectNode = CreateObject(properties.LevelUp(), index, previous);
        var result = GetValueOrArray(parent[property.Name], objectNode, property.Indexes);
        properties.DoNotMoveNext();
        return result;
    }

    private static JsonNode? CreateSimpleProperty<T>(JsonNode parent, Property property, int index, IReadOnlyList<T> previous) {
        var text = property.Line.Value;
        var valueNode = text switch {
            _ when string.IsNullOrWhiteSpace(text) => default,
            _ when text.ToLower() is "null" or "default" => default,
            _ when bool.TryParse(text, out var value) => JsonValue.Create(value),
            _ when text.StartsWith('"') => JsonValue.Create(text.Trim('"')),
            _ when text.StartsWith('\'') => JsonValue.Create(text.Trim('\'')),
            _ when text.StartsWith('{') => GetFromContext(property, text.TrimStart('{').TrimEnd('}'), index, previous, false),
            _ when text.StartsWith('[') => GetFromContext(property, text.TrimStart('[').TrimEnd(']'), index, previous, true),
            _ when int.TryParse(text, out var value) => JsonValue.Create(value),
            _ when decimal.TryParse(text, out var value) => JsonValue.Create(value),
            _ => JsonValue.Create(text)
        };
        return GetValueOrArray(parent[property.Name], valueNode, property.Indexes);
    }

    private static JsonNode GetFromContext<T>(Property property, string key, int index, IReadOnlyList<T> previous, bool asArray) {
        var values = key.Split(':');
        var contextKey = values[0];
        var indexes = Array.Empty<string>();
        if (values.Length > 1) indexes = values[1].Split(',').Select(i => i.Trim()).ToArray();
        var value = GetContextValue(property, contextKey, index, previous);
        return asArray
                ? GetFromContextAsArray(property.Line.Key, contextKey, value, indexes)
                : GetFromContextAsInstance(property.Line.Key, contextKey, value, indexes);
    }

    private static object GetContextValue<T>(Property property, string key, int index, IReadOnlyList<T> previous) {
        return key switch {
            "_previous_" => previous,
            "_index_" => index,
            _ when property.Context.TryGetValue(key, out var value) => value,
            _ => throw new InvalidDataException($"The key '{key}' was not found in the deserialization context at '{property.Line.Key}'.")
        };
    }

    private static JsonNode GetFromContextAsInstance(string sourceKey, string contextKey, object value, IReadOnlyList<string> indexes) {
        var collection = (value as IEnumerable)?.Cast<object>().ToArray() ?? Array.Empty<object>();
        return indexes.Count switch {
            > 1 => throw new InvalidDataException($"An instance value can not have more than one index at '{sourceKey}'."),
            > 0 when collection.Length == 0 => throw new InvalidDataException($"The key '{contextKey}' of the deserialization context contains a single object and can't be used with an index at '{sourceKey}'."),
            > 0 when int.TryParse(indexes[0], out var index) && index >= 0 && index < collection.Length => SerializeToNode(collection[index], new JsonSerializerOptions { IncludeFields = true })!,
            > 0 => throw new InvalidDataException($"'{indexes[0]}' is not a valid index for the collection contained in the deserialization context under '{contextKey}' at '{sourceKey}'."),
            _ when collection.Length > 0 => throw new InvalidDataException($"The key '{contextKey}' of the deserialization context contains a collection and can't be assigned to an instance without an index at '{sourceKey}'."),
            _ => SerializeToNode(value)!
        };
    }

    private static JsonNode GetFromContextAsArray(string sourceKey, string contextKey, object value, IReadOnlyCollection<string> values) {
        var collection = (value as IEnumerable)?.Cast<object>().ToArray() ?? Array.Empty<object>();
        return values.Count switch {
            > 0 when collection.Length == 0 => throw new InvalidDataException($"The key '{contextKey}' of the deserialization context does not contain a collection at '{sourceKey}'."),
            > 0 when TryGetIndexes(values, out var indexes) && indexes.All(index => index >= 0 && index < collection.Length)
                => SerializeToNode(indexes.Select(index => collection[index]).ToArray(), new JsonSerializerOptions { IncludeFields = true })!,
            > 0 => throw new InvalidDataException($"'{string.Join(',', values)}' is not a valid set of indexes for the collection contained in the deserialization context under '{contextKey}' at '{sourceKey}'."),
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