using Complex.SpecFlow.Assist.Collections;

namespace Complex.SpecFlow.Assist.Factories;

internal static class PropertyCollectionFactory {

    public static PropertyCollection CreateFromVertical(Table table) {
        var source = table.Rows.Select(i => new TableLine(i[0], i[1])).GetEnumerator();
        return new PropertyCollection(source);
    }

    public static IEnumerable<PropertyCollection> CreateFromHorizontal(Table table) {
        return table.Rows.Select(row => row.Keys.Select(key => new TableLine(key, row[key])).GetEnumerator())
            .Select(source => new PropertyCollection(source));
    }
}