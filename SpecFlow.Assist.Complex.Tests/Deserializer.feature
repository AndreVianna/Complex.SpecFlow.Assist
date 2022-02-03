Feature: Deserializer
Transforms table in complex types

@Deserializer
Scenario: Complex class with vertical table
	Given I define a table like
	| Field | Value |
	| Id    | 1     |
	When I request a complex type
	Then the result object should not be null
	And the 'Id' property should be '1'

@Deserializer
Scenario: Complex class with basic properties
	Given I define a table like
	| Field    | Value                                  |
	| Id       | 2                                      |
	| String   | "Some string."                         |
	| Integer  | 42                                     |
	| Decimal  | 3.141592                               |
	| Boolean  | True                                   |
	| DateTime | "2020-02-20T12:34:56.789"              |
	| Guid     | "1f576fa6-16c9-4905-95f8-e00cad6a8ded" |
	When I request a complex type
	Then the result object should not be null
	And the 'Id' property should be '2'
	And the 'String' property should be 'Some string.'
	And the 'Integer' property should be '42'
	And the 'Decimal' property should be '3.141592'
	And the 'Boolean' property should be 'True'
	And the 'DateTime' property should be '2020-02-20T12:34:56.789'
	And the 'Guid' property should be '1F576FA6-16C9-4905-95F8-E00CAD6A8DED'

@Deserializer
Scenario: Complex class with nullable properties
	Given I define a table like
	| Field    | Value   |
	| Id       | 3       |
	| String   | null    |
	| Integer  | NULL    |
	| Decimal  | Null    |
	| Boolean  | default |
	| DateTime |         |
	| Guid     | DEFAULT |
	| Complex  | Default |
	When I request a complex type
	Then the result object should not be null
	And the 'Id' property should be '3'
	And the 'String' property should be null
	And the 'Integer' property should be null
	And the 'Decimal' property should be null
	And the 'Boolean' property should be null
	And the 'DateTime' property should be null
	And the 'Guid' property should be null
	And the 'Complex' property should be null

@Deserializer
Scenario: Complex class with collection properties
	Given I define a table like
	| Field      | Value           |
	| Id         | 4               |
	| Lines[0]   | "Some line."    |
	| Lines[1]   | ""              |
	| Lines[2]   | "Another line." |
	| Lines[3]   | "Last line."    |
	| Numbers[0] | 101             |
	| Numbers[1] | -201            |
	| Numbers[2] | 0               |
	When I request a complex type
	Then the result object should not be null
	And the 'Id' property should be '4'
	And the 'Lines' property should have 4 items
	And the item 0 from 'Lines' should be 'Some line.'
	And the item 1 from 'Lines' should be ''
	And the item 2 from 'Lines' should be 'Another line.'
	And the item 3 from 'Lines' should be 'Last line.'
	And the 'Numbers' property should have 3 items
	And the item 0 from 'Numbers' should be '101'
	And the item 1 from 'Numbers' should be '-201'
	And the item 2 from 'Numbers' should be '0'

@Deserializer
@Deserializer
Scenario: Complex class with complex properties
	Given I define a table like
	| Field                      | Value                     |
	| Id                         | 5                         |
	| Children[0].Id             | 51                        |
	| Children[0].String         | "Some string."            |
	| Children[0].Integer        | 42                        |
	| Children[1].Id             | 52                        |
	| Children[1].Decimal        | 3.141592                  |
	| Children[1].Boolean        | False                     |
	| Children[1].DateTime       | "2020-02-20T12:34:56.789" |
	| Complex.Id                 | 53                        |
	| Complex.String             | "Some string."            |
	| Complex.Integer            | 42                        |
	| Complex.Decimal            | 3.141592                  |
	| Complex.Boolean            | True                      |
	| Complex.DateTime           | "2020-02-20T12:34:56.789" |
	| Complex.Complex.Id         | 531                       |
	| Complex.Complex.Complex.Id | 5311                      |
	When I request a complex type
	Then the result object should not be null
	And the 'Id' property should be '5'
	And the 'Children' property should have 2 items
	And the item 0 from 'Children' should be
	| Field   | Value          |
	| Id      | 51             |
	| String  | "Some string." |
	| Integer | 42             |
	And the item 1 from 'Children' should be
	| Field    | Value                     |
	| Id       | 52                        |
	| Decimal  | 3.141592                  |
	| Boolean  | False                     |
	| DateTime | "2020-02-20T12:34:56.789" |
	And the 'Complex' property should be
	| Field              | Value                     |
	| Id                 | 53                        |
	| String             | "Some string."            |
	| Integer            | 42                        |
	| Decimal            | 3.141592                  |
	| Boolean            | True                      |
	| DateTime           | "2020-02-20T12:34:56.789" |
	| Complex.Id         | 531                       |
	| Complex.Complex.Id | 5311                      |

@Deserializer
Scenario: Multi dimensional arrays
	Given I define a table like
	| Field                     | Value |
	| Id                        | 1     |
	| MultiDimensional[0][0][0] | 1     |
	| MultiDimensional[0][0][1] | 2     |
	| MultiDimensional[0][0][2] | 3     |
	| MultiDimensional[0][1][0] | 4     |
	| MultiDimensional[0][1][1] | 5     |
	| MultiDimensional[0][1][2] | 6     |
	| MultiDimensional[0][2][0] | 7     |
	| MultiDimensional[0][2][1] | 8     |
	| MultiDimensional[0][2][2] | 9     |
	| MultiDimensional[1][0][0] | 10    |
	| MultiDimensional[1][0][1] | 11    |
	| MultiDimensional[1][0][2] | 12    |
	| MultiDimensional[1][1][0] | 13    |
	| MultiDimensional[1][1][1] | 14    |
	| MultiDimensional[1][1][2] | 15    |
	| MultiDimensional[1][2][0] | 16    |
	| MultiDimensional[1][2][1] | 17    |
	| MultiDimensional[1][2][2] | 18    |
	| MultiDimensional[2][0][0] | 19    |
	| MultiDimensional[2][0][1] | 20    |
	| MultiDimensional[2][0][2] | 21    |
	| MultiDimensional[2][1][0] | 22    |
	| MultiDimensional[2][1][1] | 23    |
	| MultiDimensional[2][1][2] | 24    |
	| MultiDimensional[2][2][0] | 25    |
	| MultiDimensional[2][2][1] | 26    |
	| MultiDimensional[2][2][2] | 27    |
	When I request a complex type
	Then the 'MultiDimensional' array property should have 3 items
	And the item 0 of the 'MultiDimensional' array property should have 3 items
	And the item 0, 0 of the 'MultiDimensional' array property should have 3 items
	And the item 0, 0, 0 of the 'MultiDimensional' array property should be 1
	And the item 0, 0, 1 of the 'MultiDimensional' array property should be 2
	And the item 0, 0, 2 of the 'MultiDimensional' array property should be 3
	And the item 0, 1 of the 'MultiDimensional' array property should have 3 items
	And the item 0, 1, 0 of the 'MultiDimensional' array property should be 4
	And the item 0, 1, 1 of the 'MultiDimensional' array property should be 5
	And the item 0, 1, 2 of the 'MultiDimensional' array property should be 6
	And the item 0, 2 of the 'MultiDimensional' array property should have 3 items
	And the item 0, 2, 0 of the 'MultiDimensional' array property should be 7
	And the item 0, 2, 1 of the 'MultiDimensional' array property should be 8
	And the item 0, 2, 2 of the 'MultiDimensional' array property should be 9
	And the item 1 of the 'MultiDimensional' array property should have 3 items
	And the item 1, 0 of the 'MultiDimensional' array property should have 3 items
	And the item 1, 0, 0 of the 'MultiDimensional' array property should be 10
	And the item 1, 0, 1 of the 'MultiDimensional' array property should be 11
	And the item 1, 0, 2 of the 'MultiDimensional' array property should be 12
	And the item 1, 1 of the 'MultiDimensional' array property should have 3 items
	And the item 1, 1, 0 of the 'MultiDimensional' array property should be 13
	And the item 1, 1, 1 of the 'MultiDimensional' array property should be 14
	And the item 1, 1, 2 of the 'MultiDimensional' array property should be 15
	And the item 1, 2 of the 'MultiDimensional' array property should have 3 items
	And the item 1, 2, 0 of the 'MultiDimensional' array property should be 16
	And the item 1, 2, 1 of the 'MultiDimensional' array property should be 17
	And the item 1, 2, 2 of the 'MultiDimensional' array property should be 18
	And the item 2 of the 'MultiDimensional' array property should have 3 items
	And the item 2, 0 of the 'MultiDimensional' array property should have 3 items
	And the item 2, 0, 0 of the 'MultiDimensional' array property should be 19
	And the item 2, 0, 1 of the 'MultiDimensional' array property should be 20
	And the item 2, 0, 2 of the 'MultiDimensional' array property should be 21
	And the item 2, 1 of the 'MultiDimensional' array property should have 3 items
	And the item 2, 1, 0 of the 'MultiDimensional' array property should be 22
	And the item 2, 1, 1 of the 'MultiDimensional' array property should be 23
	And the item 2, 1, 2 of the 'MultiDimensional' array property should be 24
	And the item 2, 2 of the 'MultiDimensional' array property should have 3 items
	And the item 2, 2, 0 of the 'MultiDimensional' array property should be 25
	And the item 2, 2, 1 of the 'MultiDimensional' array property should be 26
	And the item 2, 2, 2 of the 'MultiDimensional' array property should be 27

@Deserializer
Scenario: Invalid property value
	Given I define a table like
	| Field | Value                   |
	| Id    | {Invalide Value Format} |
	When I request a complex type from an invalid table
	Then the process should throw InvalidCastException for property 'Id'
