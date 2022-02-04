namespace Complex.SpecFlow.Assist.Models;

[ExcludeFromCodeCoverage]
internal record Property(string Name, int[] Indexes, string[] Children, TableLine Line) {
    internal Property() : this(string.Empty, Array.Empty<int>(), Array.Empty<string>(), new TableLine()) { }
}