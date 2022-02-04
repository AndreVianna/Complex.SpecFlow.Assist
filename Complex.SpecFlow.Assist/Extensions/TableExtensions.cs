// ReSharper disable once CheckNamespace
namespace TechTalk.SpecFlow.Assist;

public static class TableExtensions {
    public static T CreateComplexInstance<T>(this Table table) {
        return Deserializer.Deserialize<T>(table);
    }
}
