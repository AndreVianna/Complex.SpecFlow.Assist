namespace SpecFlow.Assist.Complex.Tests.Support;

internal class ComplexObject {
    public Guid Id { get; set; }
    public string? String { get; set; }
    public int? Integer { get; set; }
    public decimal? Decimal { get; set; }
    public bool? Boolean { get; set; }
    public DateTime? DateTime { get; set; }
    public ICollection<ComplexObject>? Children { get; set; }
    public ComplexObject? Reference1 { get; set; }
}
