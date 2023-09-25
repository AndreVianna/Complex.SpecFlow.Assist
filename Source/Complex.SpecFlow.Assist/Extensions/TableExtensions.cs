// ReSharper disable once CheckNamespace
using static Complex.SpecFlow.Assist.Deserializer;

namespace TechTalk.SpecFlow.Assist;
public static class TableExtensions {
    public static T ToInstance<T>(this Table table, Func<T, T> getUpdatedInstance) {
        return table.ToInstance<T>(null, (inst, _, _) => getUpdatedInstance(inst));
    }

    public static T ToInstance<T>(this Table table, IDictionary<string, object> context, Func<T, IDictionary<string, object>, T> getUpdatedInstance) {
        return table.ToInstance<T>(context, (inst, ctx, _) => getUpdatedInstance(inst, ctx));
    }

    public static T ToInstance<T>(this Table table, Func<T, IDictionary<string, string?>, T> getUpdatedInstance) {
        return table.ToInstance<T>(null, (inst, _, extras) => getUpdatedInstance(inst, extras));
    }

    public static T ToInstance<T>(this Table table, IDictionary<string, object>? context = null, Func<T, IDictionary<string, object>, IDictionary<string, string?>, T>? getUpdatedInstance = null) {
        context ??= new Dictionary<string, object>();
        return DeserializeVertical<T>(table, context, (inst, ctx, _, _, extras) => getUpdatedInstance is not null ? getUpdatedInstance(inst, ctx, extras) : inst);
    }

    public static T[] ToArray<T>(this Table table, Func<T, T> getUpdatedInstance) {
        return table.ToArray<T>(null, (inst, _, _, _, _) => getUpdatedInstance.Invoke(inst));
    }

    public static T[] ToArray<T>(this Table table, IDictionary<string, object> context, Func<T, IDictionary<string, object>, T> getUpdatedInstance) {
        return table.ToArray<T>(context, (inst, ctx, _, _, _) => getUpdatedInstance.Invoke(inst, ctx));
    }

    public static T[] ToArray<T>(this Table table, Func<T, int, IReadOnlyList<T>, IDictionary<string, string?>, T> getUpdatedInstance) {
        return table.ToArray<T>(null, (inst, _, idx, prv, extras) => getUpdatedInstance(inst, idx, prv, extras));
    }

    public static T[] ToArray<T>(this Table table, IDictionary<string, object>? context = null, Func<T, IDictionary<string, object>, int, IReadOnlyList<T>, IDictionary<string, string?>, T>? getUpdatedInstance = null) {
        context ??= new Dictionary<string, object>();
        return DeserializeHorizontal(table, context, getUpdatedInstance ?? ((inst, _, _, _, _) => inst)).ToArray();
    }
}
