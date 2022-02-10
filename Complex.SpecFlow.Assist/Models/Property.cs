namespace Complex.SpecFlow.Assist.Models;

[ExcludeFromCodeCoverage]
internal record Property(string Name, int[] Indexes, string[] Children, TableLine Line, IDictionary<string, object> Context);

public record OnCreatedEventContext<T> {
    public OnCreatedEventContext(T instance, IDictionary<string, object> context, uint index = 0, IReadOnlyList<T>? previousItems = null, IDictionary<string, string?>? extraValues = null) {
        Context = context;
        Instance = instance;
        Index = index;
        ExtraValues = extraValues ?? new Dictionary<string, string?>(); ;
        PreviousItems = previousItems ?? new List<T>();
    }

    public IDictionary<string, object> Context { get; init; }
    public T Instance { get; init; }
    public uint Index { get; init; }
    public IReadOnlyList<T> PreviousItems { get; init; }
    public IDictionary<string, string?> ExtraValues { get; init; }
}

