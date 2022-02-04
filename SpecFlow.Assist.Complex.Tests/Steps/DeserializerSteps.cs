namespace SpecFlow.Assist.Complex.Tests.Steps;

[Binding]
[ExcludeFromCodeCoverage]
public sealed class DeserializerSteps {
    private ComplexObject? _result;
    private Action? _action;
    private Table? _table;

    [Given(@"I define a table like")]
    public void GivenIDefineATableLike(Table table) {
        _table = table;
    }

    [When(@"I request a complex type")]
    public void WhenIRequestAComplexType() {
        _result = _table!.CreateComplexInstance<ComplexObject>();
    }

    [When(@"I request a complex type with an error")]
    public void WhenIRequestAComplexTypeWithAnError() {
        _action = () => _table!.CreateComplexInstance<ComplexObject>();
    }

    [Then(@"the result object should not be null")]
    public void ThenTheResultObjectShouldBeOfTypeComplexObject() {
        _result.Should().NotBeNull();
    }

    [Then(@"the '([^']*)' property should be '([^']*)'")]
    public void ThenThePropertyShouldBe(string property, string value) {
        switch (property) {
            case "Id":
                _result!.Id.Should().Be(int.Parse(value));
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
            case "Guid":
                _result!.Guid.Should().Be(Guid.Parse(value));
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
            case "Guid":
                _result!.Guid.Should().BeNull();
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
                _result!.Lines!.Count.Should().Be(count);
                return;
            case "Numbers":
                _result!.Numbers!.Length.Should().Be(count);
                return;
            case "Children":
                _result!.Children!.Count.Should().Be(count);
                return;
            case "Items":
                _result!.Items!.Count.Should().Be(count);
                return;
            case "Dictionary":
                _result!.Dictionary!.Count.Should().Be(count);
                return;

        }
    }

    [Then(@"the item (.*) from '([^']*)' should be '([^']*)'")]
    public void ThenTheItemFromShouldBe(int index, string property, string value) {
        switch (property) {
            case "Lines":
                _result!.Lines!.ElementAt(index).Should().Be(value);
                return;
            case "Numbers":
                _result!.Numbers!.ElementAt(index).Should().Be(int.Parse(value));
                return;
        }
    }

    [Then(@"the item (.*) from '([^']*)' should be")]
    public void ThenTheItemFromShouldBe(int index, string property, ComplexObject expectedObject) {
        switch (property) {
            case "Children":
                _result!.Children!.ElementAt(index).Should().BeEquivalentTo(expectedObject);
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

    [Then(@"the item ([^,]*) of the '([^']*)' property should have (.*) items")]
    public void ThenTheItemOfTheItemsPropertyShouldHaveItems(int x, string property, int count) {
        switch (property) {
            case "Items":
                _result!.Items!.ElementAt(x).Count.Should().Be(count);
                return;
        }
    }

    [Then(@"the item ([^,]*), ([^,]*) of the 'Items' property should have (.*) items")]
    public void ThenTheItem2XOfTheItemsPropertyShouldHaveItems(int x, int y, int count) {
        _result!.Items!.ElementAt(x).ElementAt(y).Count.Should().Be(count);
    }

    [Then(@"the item ([^,]*), ([^,]*), ([^,]*) of the 'Items' property should be (.*)")]
    public void ThenTheItem3XOfTheItemsPropertyShouldBe(int x, int y, int z, int value) {
        _result!.Items!.ElementAt(x).ElementAt(y).ElementAt(z).Should().Be(value);
    }

    [Then(@"the '([^']*)' key from the '([^']*)' property should be '([^']*)'")]
    public void ThenTheKeyFromThePropertyShouldBe(string key, string property, string value) {
        switch (property) {
            case "Complex":
                _result!.Dictionary![key].Should().Be(value);
                return;
            case "SimpleTuple":
                switch (key) {
                    case "Item1":
                        _result!.SimpleTuple!.Value.Item1.Should().Be(value);
                        return;
                    case "Item2":
                        _result!.SimpleTuple!.Value.Item2.Should().Be(int.Parse(value));
                        return;
                    case "Item3":
                        _result!.SimpleTuple!.Value.Item3.Should().Be(bool.Parse(value));
                        return;
                }
                return;
            case "NamedTuple":
                switch (key) {
                    case "Name":
                        _result!.NamedTuple!.Value.Name.Should().Be(value);
                        return;
                    case "Power":
                        _result!.NamedTuple!.Value.Power.Should().Be(int.Parse(value));
                        return;
                }
                return;
        }
    }

    [Then(@"the 'Id' property of the '([^']*)' key of the item (.*) of the 'Crazy' property should be '([^']*)'")]
    public void ThenThePropertyOfTheKeyOfTheItemOfThePropertyShouldBe(string key, int index, string value) {
        _result!.Crazy!.ElementAt(index)![key].Id.Should().Be(int.Parse(value));
    }

    [Then(@"it should throw 'InvalidCastException' with message ""([^""]*)""")]
    public void ThenItShouldThrowInvalidCastExceptionWithMessage(string message) {
        _action.Should().Throw<InvalidCastException>()
            .WithMessage(message);
    }

    [Then(@"it should throw 'InvalidDataException' with message ""([^""]*)""")]
    public void ThenItShouldThrowInvalidDataExceptionWithMessage(string message) {
        _action.Should().Throw<InvalidDataException>()
            .WithMessage(message);
    }

    [StepArgumentTransformation]
    public static ComplexObject AsComplexObject(Table table)
        => table.CreateComplexInstance<ComplexObject>()!;
}
