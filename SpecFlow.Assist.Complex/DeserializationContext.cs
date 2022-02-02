namespace SpecFlow.Assist.Complex;

internal class DeserializationContext {
    public DeserializationContext(TableRows source) {
        Enumerator = source.GetEnumerator();
        Enumerator.MoveNext();
    }

    public IEnumerator<TableRow?> Enumerator { get; }
    public string? CurrentKey { get; set; }
    public string? CurrentValue { get; set; }
    public string ParentPath { get; set; } = string.Empty;

    public string CurrentToken => RelativePath.First();
    public string Name => IsArray ? CurrentToken[..CurrentToken.IndexOf('[')] : CurrentToken;
    public bool HasChildren => RelativePath.Skip(1).Any();
    public bool IsArray => CurrentToken.Contains('[');

    private IEnumerable<string> RelativePath => CurrentKey!.Remove(0, ParentPath.Length).Split('.');
}