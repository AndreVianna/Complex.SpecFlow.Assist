// ReSharper disable once CheckNamespace
namespace TechTalk.SpecFlow.Assist;

public static class TableExtensions {
    public static T CreateComplexInstance<T>(this Table table) {
        return table.CreateComplexInstance<T>(null, null);
    }


    public static T CreateComplexInstance<T>(this Table table, IDictionary<string, object> context) {
        return table.CreateComplexInstance<T>(context, null);
    }

    public static T CreateComplexInstance<T>(this Table table, Action<T> onCreated) {
        return table.CreateComplexInstance<T>(null, (inst, _, _) => onCreated.Invoke(inst));
    }

    public static T CreateComplexInstance<T>(this Table table, Action<T, IDictionary<string, object>> onCreated) {
        return table.CreateComplexInstance<T>(null, (inst, ctx, _) => onCreated.Invoke(inst, ctx));
    }

    public static T CreateComplexInstance<T>(this Table table, Action<T, IDictionary<string, object>, IDictionary<string, string?>> onCreated) {
        return table.CreateComplexInstance<T>(null, (inst, ctx, extra) => onCreated.Invoke(inst, ctx, extra));
    }

    public static T CreateComplexInstance<T>(this Table table, IDictionary<string, object>? context, Action<T, IDictionary<string, object>, IDictionary<string, string?>>? onCreated) {
        return Deserializer.DeserializeVertical<T>(table, context ?? new Dictionary<string, object>(), (inst, ctx, _, _, extra) => onCreated?.Invoke(inst, ctx, extra));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table) {
        return table.CreateComplexSet<T>(null, null).ToArray();
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, IDictionary<string, object> context) {
        return table.CreateComplexSet<T>(context, null).ToArray();
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, Action<T> onCreated) {
        return table.CreateComplexSet<T>(null, (inst, _, _, _, _) => onCreated.Invoke(inst));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, Action<T, IDictionary<string, object>> onCreated) {
        return table.CreateComplexSet<T>(null, (inst, ctx, _, _, _) => onCreated.Invoke(inst, ctx));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, Action<T, IDictionary<string, object>, int> onCreated) {
        return table.CreateComplexSet<T>(null, (inst, ctx, idx, _, _) => onCreated.Invoke(inst, ctx, idx));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, Action<T, IDictionary<string, object>, int, IReadOnlyList<T>> onCreated) {
        return table.CreateComplexSet<T>(null, (inst, ctx, idx, prv, _) => onCreated.Invoke(inst, ctx, idx, prv));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, Action<T, IDictionary<string, object>, int, IReadOnlyList<T>, IDictionary<string, string?>> onCreated) {
        return table.CreateComplexSet<T>(null, (inst, ctx, idx, prv, extra) => onCreated.Invoke(inst, ctx, idx, prv, extra));
    }

    public static IEnumerable<T> CreateComplexSet<T>(this Table table, IDictionary<string, object>? context, Action<T, IDictionary<string, object>, int, IReadOnlyList<T>, IDictionary<string, string?>>? onCreated) {
        return Deserializer.DeserializeHorizontal<T>(table, context ?? new Dictionary<string, object>(), (inst, ctx, idx, list, extra) => onCreated?.Invoke(inst, ctx, idx, list, extra)).ToArray();
    }
}
