namespace Complex.SpecFlow.Assist.Models;

[ExcludeFromCodeCoverage]
internal record TableLine(string Key, string Value) {
    internal TableLine() : this(string.Empty, string.Empty) { }
}