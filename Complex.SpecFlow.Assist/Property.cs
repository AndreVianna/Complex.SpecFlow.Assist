namespace Complex.SpecFlow.Assist;

[ExcludeFromCodeCoverage]
internal record Property(string Key, string Value, string Name, int[] Indexes, string[] Children);