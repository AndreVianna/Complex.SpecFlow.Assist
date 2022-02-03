namespace SpecFlow.Assist.Complex;

internal record PropertyInfo(string Name, int[] Indexes, string[] Children) {
    public bool IsArray => Indexes.Length > 0;
    public bool IsComplex => Children.Length > 0;
}