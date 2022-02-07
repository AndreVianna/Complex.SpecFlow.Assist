namespace Complex.SpecFlow.Assist.Tests.Steps;

[Binding]
[ExcludeFromCodeCoverage]
public sealed class DeserializerSteps {
    private ComplexObject _instance = default!;
    private IEnumerable<ComplexObject> _set = default!;
    private Action _action = default!;
    private Table _table = default!;
    private readonly IDictionary<string, object> _context = new Dictionary<string, object>();

    [Given(@"I define a table like")]
    public void GivenIDefineATableLike(Table table) {
        _table = table;
    }

    [Given(@"store as an instance in a context under '([^']*)'")]
    public void GivenStoreAsAnInstanceInAContextUnder(string key) {
        _context[key] = _table.CreateComplexInstance<ComplexObject>();
    }

    [Given(@"store as a set in a context under '([^']*)'")]
    public void GivenStoreAsASetInAContextUnder(string key) {
        _context[key] = _table.CreateComplexSet<ComplexObject>();
    }

    [When(@"I request a complex instance")]
    public void WhenIRequestAComplexInstance() {
        _instance = _table.CreateComplexInstance<ComplexObject>();
    }

    [When(@"I request a complex instance with a context")]
    public void WhenIRequestAComplexInstanceWithAContext() {
        _instance = _table.CreateComplexInstance<ComplexObject>(_context);
    }

    [When(@"I request a complex set")]
    public void WhenIRequestAComplexSet() {
        _set = _table.CreateComplexSet<ComplexObject>();
    }

    [When(@"I request a complex instance with an error")]
    public void WhenIRequestAComplexInstanceWithAnError() {
        _action = () => _table.CreateComplexInstance<ComplexObject>();
    }

    [When(@"I request a complex instance with a context with an error")]
    public void WhenIRequestAComplexInstanceWithAContextWithAnError() {
        _action = () => _table.CreateComplexInstance<ComplexObject>(_context);
    }

    [When(@"I request a complex set with an error")]
    public void WhenIRequestAComplexSetWithAnError() {
        _action = () => _table.CreateComplexSet<ComplexObject>();
    }

    [Then(@"the result object should not be null")]
    public void ThenTheResultObjectShouldBeOfTypeComplexObject() {
        _instance.Should().NotBeNull();
    }

    [Then(@"the result object should be")]
    public void ThenTheResultObjectShouldBe(Table table) {
        var result = table.CreateComplexInstance<ComplexObject>();
        _instance.Should().BeEquivalentTo(result);
    }

    [Then(@"the result collection should have (.*) items")]
    public void ThenTheResultCollectionShouldHaveItems(int count) {
        _set.Should().NotBeNull();
        _set.Count().Should().Be(count);
    }

    [Then(@"the '([^']*)' property should be '([^']*)'")]
    public void ThenThePropertyShouldBe(string property, string value) {
        switch (property) {
            case "Id":
                _instance.Id.Should().Be(int.Parse(value));
                return;
            case "String":
                _instance.String.Should().Be(value);
                return;
            case "Integer":
                _instance.Integer.Should().Be(int.Parse(value));
                return;
            case "Decimal":
                _instance.Decimal.Should().Be(decimal.Parse(value));
                return;
            case "Boolean":
                _instance.Boolean.Should().Be(bool.Parse(value));
                break;
            case "DateTime":
                _instance.DateTime.Should().Be(DateTime.Parse(value));
                return;
            case "Guid":
                _instance.Guid.Should().Be(Guid.Parse(value));
                return;
        }
    }

    [Then(@"the '([^']*)' property of the item (.*) should be '([^']*)'")]
    public void ThenThePropertyOfTheItemShouldBe(string property, int index, string value) {
        switch (property) {
            case "Id":
                _set.ElementAt(index).Id.Should().Be(int.Parse(value));
                return;
            case "String":
                _set.ElementAt(index).String.Should().Be(value);
                return;
            case "Integer":
                _set.ElementAt(index).Integer.Should().Be(int.Parse(value));
                return;
            case "Decimal":
                _set.ElementAt(index).Decimal.Should().Be(decimal.Parse(value));
                return;
            case "Boolean":
                _set.ElementAt(index).Boolean.Should().Be(bool.Parse(value));
                break;
            case "DateTime":
                _set.ElementAt(index).DateTime.Should().Be(DateTime.Parse(value));
                return;
            case "Guid":
                _set.ElementAt(index).Guid.Should().Be(Guid.Parse(value));
                return;
        }
    }

    [Then(@"the '([^']*)' property should be null")]
    public void ThenThePropertyShouldBeNull(string property) {
        switch (property) {
            case "String":
                _instance.String.Should().BeNull();
                return;
            case "Integer":
                _instance.Integer.Should().BeNull();
                return;
            case "Decimal":
                _instance.Decimal.Should().BeNull();
                return;
            case "Boolean":
                _instance.Boolean.Should().BeNull();
                break;
            case "DateTime":
                _instance.DateTime.Should().BeNull();
                return;
            case "Guid":
                _instance.Guid.Should().BeNull();
                return;
            case "Complex":
                _instance.Complex.Should().BeNull();
                return;
        }
    }

    [Then(@"the '([^']*)' property should have (.*) items")]
    public void ThenThePropertyShouldHaveItems(string property, int count) {
        switch (property) {
            case "Lines":
                _instance.Lines!.Count.Should().Be(count);
                return;
            case "Numbers":
                _instance.Numbers!.Length.Should().Be(count);
                return;
            case "Children":
                _instance.Children!.Count.Should().Be(count);
                return;
            case "Items":
                _instance.Items!.Count.Should().Be(count);
                return;
            case "Dictionary":
                _instance.Dictionary!.Count.Should().Be(count);
                return;

        }
    }

    [Then(@"the item (.*) from '([^']*)' should be '([^']*)'")]
    public void ThenTheItemFromShouldBe(int index, string property, string value) {
        switch (property) {
            case "Lines":
                _instance.Lines!.ElementAt(index).Should().Be(value);
                return;
            case "Numbers":
                _instance.Numbers!.ElementAt(index).Should().Be(int.Parse(value));
                return;
        }
    }

    [Then(@"the item (.*) from '([^']*)' should be")]
    public void ThenTheItemFromShouldBe(int index, string property, ComplexObject expectedObject) {
        switch (property) {
            case "Children":
                _instance.Children!.ElementAt(index).Should().BeEquivalentTo(expectedObject);
                return;
        }
    }

    [Then(@"the '([^']*)' property should be")]
    public void ThenThePropertyShouldBe(string property, ComplexObject expectedObject) {
        switch (property) {
            case "Complex":
                _instance.Complex.Should().BeEquivalentTo(expectedObject);
                return;
        }
    }

    [Then(@"the item ([^,]*) of the '([^']*)' property should have (.*) items")]
    public void ThenTheItemOfTheItemsPropertyShouldHaveItems(int x, string property, int count) {
        switch (property) {
            case "Items":
                _instance.Items!.ElementAt(x).Count.Should().Be(count);
                return;
        }
    }

    [Then(@"the item ([^,]*), ([^,]*) of the 'Items' property should have (.*) items")]
    public void ThenTheItem2XOfTheItemsPropertyShouldHaveItems(int x, int y, int count) {
        _instance.Items!.ElementAt(x).ElementAt(y).Count.Should().Be(count);
    }

    [Then(@"the item ([^,]*), ([^,]*), ([^,]*) of the 'Items' property should be (.*)")]
    public void ThenTheItem3XOfTheItemsPropertyShouldBe(int x, int y, int z, int value) {
        _instance.Items!.ElementAt(x).ElementAt(y).ElementAt(z).Should().Be(value);
    }

    [Then(@"the '([^']*)' key from the '([^']*)' property should be '([^']*)'")]
    public void ThenTheKeyFromThePropertyShouldBe(string key, string property, string value) {
        switch (property) {
            case "Complex":
                _instance.Dictionary![key].Should().Be(value);
                return;
            case "SimpleTuple":
                switch (key) {
                    case "Item1":
                        _instance.SimpleTuple!.Value.Item1.Should().Be(value);
                        return;
                    case "Item2":
                        _instance.SimpleTuple!.Value.Item2.Should().Be(int.Parse(value));
                        return;
                    case "Item3":
                        _instance.SimpleTuple!.Value.Item3.Should().Be(bool.Parse(value));
                        return;
                }
                return;
            case "NamedTuple":
                switch (key) {
                    case "Name":
                        _instance.NamedTuple!.Value.Name.Should().Be(value);
                        return;
                    case "Power":
                        _instance.NamedTuple!.Value.Power.Should().Be(int.Parse(value));
                        return;
                }
                return;
        }
    }

    [Then(@"the 'Id' property of the '([^']*)' key of the item (.*) of the 'Crazy' property should be '([^']*)'")]
    public void ThenThePropertyOfTheKeyOfTheItemOfThePropertyShouldBe(string key, int index, string value) {
        _instance.Crazy!.ElementAt(index)[key].Id.Should().Be(int.Parse(value));
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

    [Then(@"it should throw 'InvalidOperationException' with message ""([^""]*)""")]
    public void ThenItShouldThrowInvalidOperationExceptionWithMessage(string message) {
        _action.Should().Throw<InvalidOperationException>()
            .WithMessage(message);
    }

    [Then(@"the inner exception should be 'InvalidCastException' with message ""([^""]*)""")]
    public void ThenTheInnerExceptionShouldBeInvalidCastExceptionWithMessage(string message) {
        _action.Should().Throw<InvalidOperationException>()
            .WithInnerException<InvalidCastException>()
            .WithMessage(message);
    }

    [Then(@"the inner exception should be 'InvalidDataException' with message ""([^""]*)""")]
    public void ThenTheInnerExceptionShouldBeInvalidDataExceptionWithMessage(string message) {
        _action.Should().Throw<InvalidOperationException>()
            .WithInnerException<InvalidDataException>()
            .WithMessage(message);
    }

    [StepArgumentTransformation]
    public static ComplexObject AsComplexObject(Table table)
        => table.CreateComplexInstance<ComplexObject>();
}
