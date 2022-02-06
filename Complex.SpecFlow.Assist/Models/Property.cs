namespace Complex.SpecFlow.Assist.Models;

[ExcludeFromCodeCoverage]
internal record Property(string Name, int[] Indexes, string[] Children, TableLine Line);