// ReSharper disable once CheckNamespace
namespace TechTalk.SpecFlow.Assist;

public static class TableExtensions {
    public static T CreateComplexInstance<T>(this Table table, Func<T, T> getUpdatedInstance) {
        return table.CreateComplexInstance<T>(null, (inst, _, _) => getUpdatedInstance(inst));
    }

    public static T CreateComplexInstance<T>(this Table table, IDictionary<string, object> context, Func<T, IDictionary<string, object>, T> getUpdatedInstance) {
        return table.CreateComplexInstance<T>(context, (inst, ctx, _) => getUpdatedInstance(inst, ctx));
    }

    public static T CreateComplexInstance<T>(this Table table, Func<T, IDictionary<string, string?>, T> getUpdatedInstance) {
        return table.CreateComplexInstance<T>(null, (inst, _, extras) => getUpdatedInstance(inst, extras));
    }

    public static T CreateComplexInstance<T>(this Table table, IDictionary<string, object>? context = null, Func<T, IDictionary<string, object>, IDictionary<string, string?>, T>? getUpdatedInstance = null) {
        context ??= new Dictionary<string, object>();
        return Deserializer.DeserializeVertical<T>(table, context, (inst, ctx, _, _, extras) => getUpdatedInstance is not null ? getUpdatedInstance(inst, ctx, extras) : inst);
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, Func<T, T> getUpdatedInstance) {
        return table.CreateComplexSet<T>(null, (inst, _, _, _, _) => getUpdatedInstance.Invoke(inst));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, IDictionary<string, object> context, Func<T, IDictionary<string, object>, T> getUpdatedInstance) {
        return table.CreateComplexSet<T>(context, (inst, ctx, _, _, _) => getUpdatedInstance.Invoke(inst, ctx));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, Func<T, int, IReadOnlyList<T>, IDictionary<string, string?>, T> getUpdatedInstance) {
        return table.CreateComplexSet<T>(null, (inst, _, idx, prv, extras) => getUpdatedInstance(inst, idx, prv, extras));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, IDictionary<string, object>? context = null, Func<T, IDictionary<string, object>, int, IReadOnlyList<T>, IDictionary<string, string?>, T>? getUpdatedInstance = null) {
        context ??= new Dictionary<string, object>();
        return Deserializer.DeserializeHorizontal<T>(table, context, getUpdatedInstance ?? ((inst, _, _, _, _) => inst)).ToArray();
    }
}
