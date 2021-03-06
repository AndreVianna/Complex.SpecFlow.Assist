namespace Complex.SpecFlow.Assist.Tests.Steps;

[Binding]
[ExcludeFromCodeCoverage]
public sealed class DeserializerSteps {
    private ComplexObject _instance = default!;
    private IEnumerable<ComplexObject> _set = default!;
    private IEnumerable<string> _primitives = default!;
    private Action _action = default!;
    private Table _table = default!;
    private readonly IDictionary<string, object> _context = new Dictionary<string, object>();
    private readonly ScenarioContext _scenarioContext;
    public DeserializerSteps(ScenarioContext scenarioContext) {
        _scenarioContext = scenarioContext;
    }

    [Given(@"I define a table like")]
    public void GivenIDefineATableLike(Table table) {
        _table = table;
    }

    [Given(@"store as an instance in a context under '([^']*)'")]
    public void GivenStoreAsAnInstanceInAContextUnder(string key) {
        _context[key] = _table.ToInstance<ComplexObject>();
    }

    [Given(@"store as an instance in the ScenarioContext under '([^']*)'")]
    public void GivenStoreAsAnInstanceInTheScenarioContextUnder(string key) {
        _scenarioContext[key] = _table.ToInstance<ComplexObject>();
    }

    [Given(@"store as a set in a context under '([^']*)'")]
    public void GivenStoreAsASetInAContextUnder(string key) {
        _context[key] = _table.ToArray<ComplexObject>();
    }

    [Given(@"store as a set of strings in a context under '([^']*)'")]
    public void GivenStoreAsASetOfStringsInAContextUnder(string key) {
        _context[key] = _table.ToArray<string>();
    }

    [When(@"I request a complex instance")]
    public void WhenIRequestAComplexInstance() {
        _instance = _table.ToInstance<ComplexObject>();
    }

    [When(@"I request a complex instance with a context")]
    public void WhenIRequestAComplexInstanceWithAContext() {
        _instance = _table.ToInstance<ComplexObject>(_context);
    }

    [When(@"I request a complex instance using ScenarioContext")]
    public void WhenIRequestAComplexInstanceUsingScenarioContext() {
        _instance = _table.ToInstance<ComplexObject>(_scenarioContext);
    }

    [When(@"I request a complex instance with a delegate")]
    public void WhenIRequestAComplexInstanceWitAhDelegate() {
        _instance = _table.ToInstance<ComplexObject>(instance => {
            instance.String = "Set during config.";
            return instance;
        });
    }

    [When(@"I request a complex instance with a delegate using context")]
    public void WhenIRequestAComplexInstanceWithADelegateUsingContext() {
        _instance = _table.ToInstance<ComplexObject>(_scenarioContext, (instance, context) => {
            instance.Integer = ((ComplexObject)context["StoredObject"]).Integer;
            return instance;
        });
    }

    [When(@"I request a complex instance with a delegate using extras")]
    public void WhenIRequestAComplexInstanceWithADelegateUsingExtras() {
        _instance = _table.ToInstance<ComplexObject>((instance, extras) => {
            instance.Integer = int.Parse(extras["Value"]!);
            return instance;
        });
    }

    [When(@"I request a complex instance with an error")]
    public void WhenIRequestAComplexInstanceWithAnError() {
        _action = () => _table.ToInstance<ComplexObject>();
    }

    [When(@"I request a complex instance with a context with an error")]
    public void WhenIRequestAComplexInstanceWithAContextWithAnError() {
        _action = () => _table.ToInstance<ComplexObject>(_context);
    }

    [When(@"I request a complex set")]
    public void WhenIRequestAComplexSet() {
        _set = _table.ToArray<ComplexObject>();
    }

    [When(@"I request a complex set with a context")]
    public void WhenIRequestAComplexSetWithAContext() {
        _set = _table.ToArray<ComplexObject>(_context);
    }

    [When(@"I request a complex set with a delegate")]
    public void WhenIRequestAComplexSetWithADelegate() {
        _set = _table.ToArray<ComplexObject>(
            instance => {
                instance.String = "Some fixed value.";
                instance.Decimal = 3.141592m;
                return instance;
            });
    }

    [When(@"I request a complex set with a delegate using context")]
    public void WhenIRequestAComplexSetWithADelegateUsingContext() {
        _set = _table.ToArray<ComplexObject>(_scenarioContext,
            (instance, context) => {
                instance.Integer = ((ComplexObject)context["StoredObject"]).Integer;
                return instance;
            });
    }

    [When(@"I request a complex set with a delegate using extras")]
    public void WhenIRequestAComplexSetWithADelegateUsingExtra() {
        _set = _table.ToArray<ComplexObject>(
            (instance, index, _, extra) => {
                instance.String = $"Set during config at index {index}.";
                instance.Decimal = extra["Values"] switch {
                    "Pi" => 3.141592m,
                    "Tau" => 6.283185m,
                    _ => null
                };
                return instance;
            });
    }

    [When(@"I request a complex set with an error")]
    public void WhenIRequestAComplexSetWithAnError() {
        _action = () => _table.ToArray<ComplexObject>();
    }

    [When(@"I request a set of strings")]
    public void WhenIRequestASetOfStrings() {
        _primitives = _table.ToArray<string>();
    }

    [Then(@"the item (\d+) should be '([^']*)'")]
    public void ThenTheItemShouldBe(int index, string value) {
        _primitives.ElementAt(index).Should().Be(value);
    }

    [Then(@"the result object should not be null")]
    public void ThenTheResultObjectShouldBeOfTypeComplexObject() {
        _instance.Should().NotBeNull();
    }

    [Then(@"the result object should be")]
    public void ThenTheResultObjectShouldBe(Table table) {
        var result = table.ToInstance<ComplexObject>();
        _instance.Should().BeEquivalentTo(result);
    }

    [Then(@"the result collection of strings should have (\d+) items")]
    public void ThenTheResultCollectionOfStringsShouldHaveItems(int count) {
        _primitives.Should().NotBeNull();
        _primitives.Count().Should().Be(count);
    }

    [Then(@"the result collection should have (\d+) items")]
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

    [Then(@"the '([^']*)' property of the item (\d+) should be null")]
    public void ThenThePropertyOfTheItemShouldBeNull(string property, int index) {
        switch (property) {
            case "Decimal":
                _set.ElementAt(index).Decimal.Should().BeNull();
                return;
        }
    }

    [Then(@"the '([^']*)' property of the item (\d+) should be '([^']*)'")]
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

    [Then(@"the '([^']*)' property should have (\d+) items")]
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

    [Then(@"the '([^']*)' property of the item (\d+) should have (\d+) items")]
    public void ThenThePropertyOfTheItemShouldHaveItems(string property, int index, int count) {
        switch (property) {
            case "Lines":
                _set.ElementAt(index).Lines!.Count.Should().Be(count);
                return;
            case "Numbers":
                _set.ElementAt(index).Numbers!.Length.Should().Be(count);
                return;
            case "Children":
                _set.ElementAt(index).Children!.Count.Should().Be(count);
                return;
            case "Items":
                _set.ElementAt(index).Items!.Count.Should().Be(count);
                return;
            case "Dictionary":
                _set.ElementAt(index).Dictionary!.Count.Should().Be(count);
                return;
        }
    }

    [Then(@"the item (\d+) from '([^']*)' should be '([^']*)'")]
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

    [Then(@"the item (\d+) from '([^']*)' should be")]
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

    [Then(@"the item ([^,]*) of the '([^']*)' property should have (\d+) items")]
    public void ThenTheItemOfTheItemsPropertyShouldHaveItems(int x, string property, int count) {
        switch (property) {
            case "Items":
                _instance.Items!.ElementAt(x).Count.Should().Be(count);
                return;
        }
    }

    [Then(@"the item ([^,]*), ([^,]*) of the 'Items' property should have (\d+) items")]
    public void ThenTheItem2XOfTheItemsPropertyShouldHaveItems(int x, int y, int count) {
        _instance.Items!.ElementAt(x).ElementAt(y).Count.Should().Be(count);
    }

    [Then(@"the item ([^,]*), ([^,]*), ([^,]*) of the 'Items' property should be '([^']*)'")]
    public void ThenTheItem3XOfTheItemsPropertyShouldBe(int x, int y, int z, string value) {
        _instance.Items!.ElementAt(x).ElementAt(y).ElementAt(z).Should().Be(int.Parse(value));
    }

    [Then(@"the '([^']*)' key from the '([^']*)' property should be '([^']*)'")]
    public void ThenTheKeyFromThePropertyShouldBe(string key, string property, string value) {
        switch (property) {
            case "SimpleTuple":
                switch (key) {
                    case "Item1":
                        _instance.SimpleTuple!.Value.Item1.Should().Be(value);
                        break;
                    case "Item2":
                        _instance.SimpleTuple!.Value.Item2.Should().Be(int.Parse(value));
                        break;
                    case "Item3":
                        _instance.SimpleTuple!.Value.Item3.Should().Be(bool.Parse(value));
                        break;
                }
                return;
            case "NamedTuple":
                switch (key) {
                    case "Name":
                        _instance.NamedTuple!.Value.Name.Should().Be(value);
                        break;
                    case "Power":
                        _instance.NamedTuple!.Value.Power.Should().Be(int.Parse(value));
                        break;
                }
                return;
        }
    }

    [Then(@"the 'Id' property of the '([^']*)' key of the item (\d+) of the 'Crazy' property should be '([^']*)'")]
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
        => table.ToInstance<ComplexObject>();
}
