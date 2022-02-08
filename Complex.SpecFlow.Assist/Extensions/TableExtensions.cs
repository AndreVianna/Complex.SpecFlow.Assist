// ReSharper disable once CheckNamespace
namespace TechTalk.SpecFlow.Assist;

public static class TableExtensions {
    public static T CreateComplexInstance<T>(this Table table) {
        return table.CreateComplexInstance<T>(null, null);
    }

    public static T CreateComplexInstance<T>(this Table table, IDictionary<string, object> context) {
        return table.CreateComplexInstance<T>(context, null);
    }

    public static T CreateComplexInstance<T>(this Table table, Action<T, IDictionary<string, object>> onCreated) {
        return table.CreateComplexInstance<T>(null, onCreated);
    }

    private static void NullInstanceConfiguration<T>(T instance, IDictionary<string, object> context) { }
    public static T CreateComplexInstance<T>(this Table table, IDictionary<string, object>? context, Action<T, IDictionary<string, object>>? onCreated) {
        return Deserializer.DeserializeVertical<T>(table, context ?? new Dictionary<string, object>(), onCreated ?? NullInstanceConfiguration);
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table) {
        return table.CreateComplexSet<T>(null, null).ToArray();
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, IDictionary<string, object> context) {
        return table.CreateComplexSet<T>(context, null).ToArray();
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, Action<T, int, IDictionary<string, object>>? onCreated) {
        return table.CreateComplexSet<T>(null, onCreated).ToArray();
    }

    private static void NullItemConfiguration<T>(T instance, int index, IDictionary<string, object> context) { }
    public static IEnumerable<T> CreateComplexSet<T>(this Table table, IDictionary<string, object>? context, Action<T, int, IDictionary<string, object>>? onCreated) {
        return Deserializer.DeserializeHorizontal<T>(table, context ?? new Dictionary<string, object>(), onCreated ?? NullItemConfiguration).ToArray();
    }
}
