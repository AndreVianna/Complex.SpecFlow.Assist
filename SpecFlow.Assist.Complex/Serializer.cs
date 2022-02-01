namespace SpecFlow.Assist.Complex;

internal static class Serializer {
    internal static IDictionary<string, string> SerializeToDictionary<T>(T value) {
        var json = SerializeToElement(value);
        var result = new Dictionary<string, string>();
        ProcessObject(result, json);
        return result;
    }

    private static void ProcessElement(Dictionary<string, string> result, JsonElement node, string member, string? parent = null) {
        var key = parent is null ? member : $"{parent}.{member}";
        switch (node.ValueKind) {
            case JsonValueKind.Object:
                ProcessObject(result, node, key);
                return;
            case JsonValueKind.Array:
                ProcessArray(result, node, key);
                return;
            case JsonValueKind.True or JsonValueKind.False or JsonValueKind.Null or JsonValueKind.Undefined:
                result.Add(key, node.ValueKind.ToString());
                return;
            case JsonValueKind.Number:
                result.Add(key, node.GetRawText());
                return;
            default:
                result.Add(key, $"\"{node.GetString()}\"");
                return;
        }
    }

    private static void ProcessArray(Dictionary<string, string> result, JsonElement array, string member) {
        var length = array.GetArrayLength();
        for (var i = 0; i < length; i++) {
            var item = $"{member}[{i}]";
            ProcessElement(result, array[i], item);
        }
    }

    private static void ProcessObject(Dictionary<string, string> result, JsonElement node, string? parent = null) {
        var dic = node.Deserialize<IDictionary<string, JsonElement>>()!;
        foreach (var (key, value) in dic) {
            ProcessElement(result, value, key, parent);
        }
    }
}