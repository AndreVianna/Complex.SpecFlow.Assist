namespace SpecFlow.Assist.Complex.Tests.Support;

public class ComplexObject {
    public Guid Id { get; set; }
    public string? String { get; set; }
    public int? Integer { get; set; }
    public decimal? Decimal { get; set; }
    public bool? Boolean { get; set; }
    public DateTime? DateTime { get; set; }
    public ICollection<string> Lines { get; set; } = new List<string>();
    public ICollection<int> Numbers { get; set; } = new List<int>();
    public ICollection<ComplexObject> Children { get; set; } = new List<ComplexObject>();
    public ComplexObject? Complex { get; set; }
}
