namespace SpecFlow.Assist.Complex.Tests.Steps;

[Binding]
public sealed class DeserializerSteps {
    private ComplexObject? _result;

    [When(@"I define a table like")]
    public void WhenIDefineATableLike(Table table) {
        _result = table.CreateComplexInstance<ComplexObject>();
    }

    [Then(@"the result object should not be null")]
    public void ThenTheResultObjectShouldBeOfTypeComplexObject() {
        _result.Should().NotBeNull();
    }

    [Then(@"the '([^']*)' property should be '([^']*)'")]
    public void ThenThePropertyShouldBe(string property, string value) {
        switch (property) {
            case "Id":
                _result!.Id.Should().Be(Guid.Parse(value));
                break;
            case "String":
                _result!.String.Should().Be(value);
                break;
            case "Integer":
                _result!.Integer.Should().Be(int.Parse(value));
                break;
            case "Decimal":
                _result!.Decimal.Should().Be(decimal.Parse(value));
                break;
            case "Boolean":
                _result!.Boolean.Should().Be(bool.Parse(value));
                break;
            case "DateTime":
                _result!.DateTime.Should().Be(DateTime.Parse(value));
                break;
        }
    }

}
