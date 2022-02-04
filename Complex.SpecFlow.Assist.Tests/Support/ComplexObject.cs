namespace Complex.SpecFlow.Assist.Tests.Support;

public class ComplexObject {
    public int Id { get; set; }
    public string? String { get; set; }
    public int? Integer { get; set; }
    public decimal? Decimal { get; set; }
    public bool? Boolean { get; set; }
    public DateTime? DateTime { get; set; }
    public Guid? Guid { get; set; }
    public ICollection<string>? Lines { get; set; }
    public int[]? Numbers { get; set; }
    public ICollection<ComplexObject>? Children { get; set; }
    public ComplexObject? Complex { get; set; }
    public ICollection<ICollection<ICollection<int>>>? Items { get; set; }
    public IDictionary<string, string>? Dictionary { get; set; }
    public (string Name, int Power)? NamedTuple { get; set; }
    public (string, int, bool)? SimpleTuple { get; set; }
    public ICollection<IDictionary<string, ComplexObject>>? Crazy { get; set; }
}
