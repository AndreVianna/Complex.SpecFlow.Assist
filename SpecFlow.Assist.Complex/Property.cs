namespace SpecFlow.Assist.Complex;

[ExcludeFromCodeCoverage]
internal record Property(string Key, string Value, string Name, int[] Indexes, string[] Children);