using static Complex.SpecFlow.Assist.Collections.PropertyCollection.TableDirection;

namespace Complex.SpecFlow.Assist.Factories;

internal static class PropertyCollectionFactory {

    public static PropertyCollection CreateFromVertical(Table table, IDictionary<string, object> context) {
        var source = table.Rows.Select(i => new TableLine(i[0], i[1])).GetEnumerator();
        return new PropertyCollection(source, table.Rows.Count, Vertical, context: context);
    }

    public static IEnumerable<PropertyCollection> CreateFromHorizontal(Table table, IDictionary<string, object> context) {
        var lines = table.Rows.Select(row => row.Keys.Select(key => new TableLine(key, row[key])).GetEnumerator());
        return lines.Select(source => new PropertyCollection(source, table.Header.Count, Horizontal, context: context));
    }
}