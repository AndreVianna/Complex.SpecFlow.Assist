namespace SpecFlow.Assist.Complex;

internal static class Deserializer {
    internal static T? Deserialize<T>(Table table) {
        var context = new Context(table.Rows.GetEnumerator());
        context.MoveNext();
        var result = CreateObject(context);
        return result.Deserialize<T>();
    }

    private static JsonNode CreateObject(Context context) {
        var result = new JsonObject();
        while (context.HasMore) {
            AddPropertyTo(context, result);
        }

        return result;
    }

    private static void AddPropertyTo(Context context, JsonNode targetNode) {
        var property = context.GetCurrentPropertyInfo();
        if (property.IsComplex) {
            AddComplexProperty(context, targetNode, property);
            context.UpdateCurrent();
        }
        else {
            AddValueNode(context, targetNode, property);
            context.MoveNext();
        }
    }

    private static void AddComplexProperty(Context context, JsonNode targetNode, PropertyInfo property) {
        var newContext = new Context(context.Enumerator, context.Level + 1);
        var complexValue = CreateObject(newContext);
        var value = GetFinalValue(context, targetNode[property.Name], complexValue, property.Indexes, context.PreviousLineIndexes);
        targetNode[property.Name] = value;
    }

    private static void AddValueNode(Context context, JsonNode targetNode, PropertyInfo property) {
        var valueNode = context.CurrentValue switch {
            _ when context.CurrentValue!.ToLower() == "null" => default,
            _ when context.CurrentValue.ToLower() == "default" => default,
            _ when string.IsNullOrWhiteSpace(context.CurrentValue) => default,
            _ when context.CurrentValue.ToLower() == "true" => JsonValue.Create(true),
            _ when context.CurrentValue.ToLower() == "false" => JsonValue.Create(false),
            _ when context.CurrentValue.StartsWith("\"") => JsonValue.Create(context.CurrentValue.TrimStart('"')
                .TrimEnd('"')),
            _ when int.TryParse(context.CurrentValue, out var number) => JsonValue.Create(number),
            _ when double.TryParse(context.CurrentValue, out var number) => JsonValue.Create(number),
            _ => throw new InvalidCastException($"Invalid value at '{context.CurrentKey}'."),
        };
        var value = GetFinalValue(context, targetNode[property.Name], valueNode, property.Indexes, context.PreviousLineIndexes);
        targetNode[property.Name] = value;
    }

    private static JsonNode? GetFinalValue(Context context, JsonNode? targetNode, JsonNode? valueNode, int[] indexes, int[] previousIndexes) {
        return indexes.Any()
            ? CreateOrUpdateArray(context, targetNode, valueNode, indexes, previousIndexes)
            : valueNode;
    }

    private static JsonNode CreateOrUpdateArray(Context context, JsonNode? targetNode, JsonNode? valueNode, int[] indexes, int[] previousIndexes) {
        var subIndexes = indexes.Skip(1).ToArray();
        var currentIndex = indexes.First();
        EnsureIndexChange(context, currentIndex, previousIndexes.First(), subIndexes);
        var array = targetNode?.AsArray() ?? new JsonArray();
        var value = GetFinalValue(context, currentIndex < array.Count ? array[currentIndex] : null, valueNode, subIndexes, previousIndexes.Skip(1).ToArray());
        if (currentIndex == array.Count) array.Add(value);
        return array;
    }

    private static void EnsureIndexChange(Context context, int currentIndex, int previousIndex, int[] subIndexes) {
        var indexChange = currentIndex - previousIndex;
        switch (indexChange) {
            case < 0 or > 1:
                throw new InvalidDataException($"Invalid array index at '{context.CurrentKey}'.");
            case 0 when !subIndexes.Any():
                throw new InvalidDataException($"Duplicated array index at '{context.CurrentKey}'.");
        }
    }
}