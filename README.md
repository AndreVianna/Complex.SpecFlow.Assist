# Complex.SpecFlow.Assist

Main: [![Buid Status Master](https://github.com/andrevianna/SpecFlow.Assist.Complex/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/andrevianna/SpecFlow.Assist.Complex/actions)

Development: [![Buid Status Development](https://github.com/andrevianna/SpecFlow.Assist.Complex/actions/workflows/build.yml/badge.svg?branch=development)](https://github.com/andrevianna/SpecFlow.Assist.Complex/actions)

Complex.SpecFlow.Assist adds extension methods to the Table object to help create complex objects from a vertical table definition.

It supports collectios, complex properties, dictionaries and even tuples.

You need to specify a vertical table using a flat hierarchical description of the object.

## The basics:
Here is an example:\
Consider a class defined as:
```c#
public class Basic {
    public int Id { get; set; }
    public string String { get; set; }
    public int Integer { get; set; }
    public decimal Decimal { get; set; }
    public bool Boolean { get; set; }
    public DateTime DateTime { get; set; }
    public Guid Guid { get; set; }
}
```

In the `.feature` file, you could define the table as:
```Gherkin
Given I define a table like
| Field    | Value                                  |
| Id       | 2                                      |
| String   | "Some string."                         |
| Integer  | 42                                     |
| Decimal  | 3.141592                               |
| Boolean  | True                                   |
| DateTime | "2020-02-20T12:34:56.789"              |
| Guid     | "1f576fa6-16c9-4905-95f8-e00cad6a8ded" |
```
The names in the header are ignored.

And in the steps definition you could define the method as:

```c#
    [Given(@"I define a table like")]
    public void GivenIDefineATableLike(Table table) {
        var inut = table.CreateComplexInstance<Basic>();

        // input will be seeded with the above table.
    }
```

You could also create a argument tranformation like:

```c#
    [StepArgumentTransformation]
    public static Basic AsComplexObject(Table table)
        => table.CreateComplexInstance<Basic>()!;
```

And instead, define the above method as:
```c#
    [Given(@"I define a table like")]
    public void GivenIDefineATableLike(Basic input) {

        // input will be seeded with the above table.
    }
```

## Very complex classes

Here are samples of some complex structures supported by the library:

### Sample 1 - Nullable values
Any case insensitive combination of `null`, `default`, an empty cell, or by simply not adding the property to the list would add a null value to the nullable field.
For empty strings use `""`.

```c#
public class Nullables {
    public string? String { get; set; }
    public int? Integer { get; set; }
    public decimal? Decimal { get; set; }
    public bool? Boolean { get; set; }
    public DateTime? DateTime { get; set; }
    public Guid? Guid { get; set; }
}
```

In the `.feature` file, you could define the table as:
```
| Field    | Value   |
| String   | null    |
| Integer  | NULL    |
| Decimal  | Null    |
| Boolean  | default |
| DateTime |         |
| Guid     | DEFAULT |
| Complex  | Default |
```

### Sample 2 - Collections
Any Collections interfaces (like `ICollection<>`, `IEnumerble<>`, etc) or concrete types (like `Arrays`, `Lists<>`, etc) are supported.
Even multi-dimentional collections are supported.

```c#
public class Colletions {
    public ICollection<string>? Lines { get; set; }
    public int[]? Numbers { get; set; }
    public ICollection<ICollection<ICollection<int>>>? Items { get; set; }
}
```

In the `.feature` file, you could define the table as:
```
| Field          | Value           |
| Lines[0]       | "Some line."    |
| Lines[1]       | ""              |
| Lines[2]       | "Another line." |
| Lines[3]       | "Last line."    |
| Numbers[0]     | 101             |
| Numbers[1]     | -201            |
| Numbers[2]     | 0               |
| Items[0][0][0] | 1               |
| Items[0][0][1] | 2               |
| Items[0][0][2] | 3               |
| Items[0][1][0] | 4               |
| Items[0][1][1] | 5               |
| Items[0][1][2] | 6               |
| Items[1][0][0] | 7               |
| Items[1][0][1] | 8               |
| Items[1][0][2] | 9               |
| Items[1][1][0] | 10              |
| Items[1][1][1] | 11              |
| Items[1][1][2] | 12              |
| Items[1][2][0] | 13              |
| Items[1][2][1] | 14              |
| Items[1][2][2] | 15              |
| Items[2][0][0] | 16              |
| Items[2][0][1] | 17              |
| Items[2][0][2] | 18              |
| Items[2][1][0] | 19              |
| Items[2][1][1] | 20              |
| Items[2][1][2] | 21              |
| Items[2][2][0] | 22              |
| Items[2][2][1] | 23              |
| Items[2][2][2] | 24              |
| Items[3][0][0] | 25              |
| Items[3][0][1] | 26              |
| Items[3][0][2] | 27              |
```

The indexes must be defined in a sequence.
That means they must start at 0 and always increment by one.

A table with an invalid index will throw an `InvalidDataException`.

### Sample 3 - Complex properties
Any combination os complex classes or collections are supported including self reference types.

```c#
public class Colletions {
    public int Id { get; set; }
    public Basic? Complex { get; set; }
    public ICollection<Nullables>? Children { get; set; }
    public Complex? Complex { get; set; }
}
```

In the `.feature` file, you could define the table as:
```
| Field                      | Value                                  |
| Id                         | 5                                      |
| Basic.Id                   | 50                                     |
| Basic.String               | "Some string."                         |
| Basic.Integer              | 42                                     |
| Basic.Decimal              | 3.141592                               |
| Basic.Boolean              | True                                   |
| Basic.DateTime             | "2020-02-20T12:34:56.789"              |
| Basic.Guid                 | "1f576fa6-16c9-4905-95f8-e00cad6a8ded" |
| Children[0].Id             | 51                                     |
| Children[0].String         | "Some string."                         |
| Children[0].Integer        | 42                                     |
| Children[1].Id             | 52                                     |
| Children[1].Decimal        | 3.141592                               |
| Children[1].Boolean        | False                                  |
| Children[1].DateTime       | "2020-02-20T12:34:56.789"              |
| Complex.Id                 | 53                                     |
| Complex.Basic.Id           | 531                                    |
| Complex.Basic.String       | "Some string."                         |
| Complex.Basic.Integer      | 42                                     |
| Complex.Basic.Decimal      | 3.141592                               |
| Complex.Basic.Boolean      | True                                   |
| Complex.Basic.DateTime     | "2020-02-20T12:34:56.789"              |
| Complex.Basic.Guid         | "1f576fa6-16c9-4905-95f8-e00cad6a8ded" |
| Complex.Complex.Id         | 532                                    |
| Complex.Complex.Complex.Id | 5321                                   |
```

As you can see the conbination of complex properties and collections can produce a wild range of possibilities.

### Sample 4 - Dictionaries and Tuples
Dictionaries are defined the same way that complex properties are, but the keys are not limited to the names of the fields/properties in a strong typed property.
Tuples must follow the numeric item name ('Item1', 'Item2', etc). Even named tuples have to folow that definition, although the final object will have the named fields correctly filled.

```c#
public class Colletions {
    public IDictionary<string, string?> Family { get; set; }
    public (int Number, string Street) Address { get; set; }
    public ICollection<(int, int)> Points { get; set; }
}
```

In the `.feature` file, you could define the table as:
```
| Field           | Value         |
| Family.Father   | "John"        |
| Family.Mother   | "Jane"        |
| Family.Son      | "Billy"       |
| Family.Daughter | "Cindy"       |
| Address.Item1   | 555           |
| Address.Item2   | "Back Street" |
| Points[0].Item1 | 100           |
| Points[0].Item2 | 200           |
| Points[1].Item1 | 300           |
| Points[1].Item2 | 400           |
| Points[2].Item1 | 500           |
| Points[2].Item2 | 600           |
```
