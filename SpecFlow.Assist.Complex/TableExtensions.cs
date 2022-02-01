namespace TechTalk.SpecFlow.Assist;

public static class TableExtensions {
    public static T? CreateComplexInstance<T>(this Table table) {
        var data = table.Rows.ToDictionary(i => i[0], i => i[1]);
        return Deserializer.Deserialize<T>(data);
    }
}
