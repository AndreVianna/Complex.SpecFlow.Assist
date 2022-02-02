namespace SpecFlow.Assist.Complex.Tests.Steps;

[Binding]
[ExcludeFromCodeCoverage]
public sealed class DeserializerSteps {
    private ComplexObject? _result;
    private Action? _action;

    [When(@"I define a table like")]
    public void WhenIDefineATableLike(Table table) {
        _result = table.CreateComplexInstance<ComplexObject>();
    }

    [When(@"I define an invalid table like")]
    public void WhenIDefineAnInvalidTableLike(Table table) {
        _action = () => table.CreateComplexInstance<ComplexObject>();
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
                return;
            case "String":
                _result!.String.Should().Be(value);
                return;
            case "Integer":
                _result!.Integer.Should().Be(int.Parse(value));
                return;
            case "Decimal":
                _result!.Decimal.Should().Be(decimal.Parse(value));
                return;
            case "Boolean":
                _result!.Boolean.Should().Be(bool.Parse(value));
                break;
            case "DateTime":
                _result!.DateTime.Should().Be(DateTime.Parse(value));
                return;
        }
    }

    [Then(@"the '([^']*)' property should be null")]
    public void ThenThePropertyShouldBeNull(string property) {
        switch (property) {
            case "String":
                _result!.String.Should().BeNull();
                return;
            case "Integer":
                _result!.Integer.Should().BeNull();
                return;
            case "Decimal":
                _result!.Decimal.Should().BeNull();
                return;
            case "Boolean":
                _result!.Boolean.Should().BeNull();
                break;
            case "DateTime":
                _result!.DateTime.Should().BeNull();
                return;
            case "Complex":
                _result!.Complex.Should().BeNull();
                return;
        }
    }

    [Then(@"the '([^']*)' property should have (.*) items")]
    public void ThenThePropertyShouldHaveItems(string property, int count) {
        switch (property) {
            case "Lines":
                _result!.Lines.Count.Should().Be(count);
                return;
            case "Numbers":
                _result!.Numbers.Count.Should().Be(count);
                return;
            case "Children":
                _result!.Children.Count.Should().Be(count);
                return;
        }
    }

    [Then(@"the item (.*) from '([^']*)' should be '([^']*)'")]
    public void ThenTheItemFromShouldBe(int index, string property, string value) {
        switch (property) {
            case "Lines":
                _result!.Lines.ElementAt(index).Should().Be(value);
                return;
            case "Numbers":
                _result!.Numbers.ElementAt(index).Should().Be(int.Parse(value));
                return;
        }
    }

    [Then(@"the item (.*) from '([^']*)' should be")]
    public void ThenTheItemFromShouldBe(int index, string property, ComplexObject expectedObject) {
        switch (property) {
            case "Children":
                _result!.Children.ElementAt(index).Should().BeEquivalentTo(expectedObject);
                return;
        }
    }

    [Then(@"the '([^']*)' property should be")]
    public void ThenThePropertyShouldBe(string property, ComplexObject expectedObject) {
        switch (property) {
            case "Complex":
                _result!.Complex.Should().BeEquivalentTo(expectedObject);
                return;
        }
    }

    [Then(@"the process should throw InvalidCastException for property '([^']*)'")]
    public void ThenTheProcessShouldThrowInvalidCastExceptionForProperty(string property) {
        _action.Should().Throw<InvalidCastException>()
            .WithMessage($"Invalid value for property '{property}'.");
    }

    [StepArgumentTransformation]
    public static ComplexObject AsComplexObject(Table table)
        => table.CreateComplexInstance<ComplexObject>()!;
}
