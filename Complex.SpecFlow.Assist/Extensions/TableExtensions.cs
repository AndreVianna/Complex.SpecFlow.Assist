// ReSharper disable once CheckNamespace
namespace TechTalk.SpecFlow.Assist;

public static class TableExtensions {
    public static T CreateComplexInstance<T>(this Table table, IDictionary<string, object>? context = null) {
        return Deserializer.DeserializeVertical<T>(table, context ?? new Dictionary<string, object>());
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, IDictionary<string, object>? context = null) {
        return Deserializer.DeserializeHorizontal<T>(table, context ?? new Dictionary<string, object>()).ToArray();
    }
}
