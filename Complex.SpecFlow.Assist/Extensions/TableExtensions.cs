// ReSharper disable once CheckNamespace
namespace TechTalk.SpecFlow.Assist;

public static class TableExtensions {
    public static T CreateComplexInstance<T>(this Table table) {
        return Deserializer.DeserializeVertical<T>(table);
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table) {
        return Deserializer.DeserializeHorizontal<T>(table).ToArray();
    }
}
