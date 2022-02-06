Feature: Vertical deserializer
Transforms a vertical table in complex object

@Deserializer
Scenario: Using vertical table
	Given I define a table like
	| Field | Value |
	| Id    | 1     |
	When I request a complex instance
	Then the result object should not be null
	And the 'Id' property should be '1'

@Deserializer
Scenario: With basic properties
	Given I define a table like
	| Field    | Value                                |
	| Id       | 2                                    |
	| String   | Some string.                       |
	| Integer  | 42                                   |
	| Decimal  | 3.141592                             |
	| Boolean  | True                                 |
	| DateTime | '2020-02-20T12:34:56.789'            |
	| Guid     | "1f576fa6-16c9-4905-95f8-e00cad6a8ded" |
	When I request a complex instance
	Then the result object should not be null
	And the 'Id' property should be '2'
	And the 'String' property should be 'Some string.'
	And the 'Integer' property should be '42'
	And the 'Decimal' property should be '3.141592'
	And the 'Boolean' property should be 'True'
	And the 'DateTime' property should be '2020-02-20T12:34:56.789'
	And the 'Guid' property should be '1F576FA6-16C9-4905-95F8-E00CAD6A8DED'

@Deserializer
Scenario: With nullable properties
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
	When I request a complex instance
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
Scenario: With collection properties
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
	When I request a complex instance
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
Scenario: With complex properties
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
	| Complex.Complex.Id         | 531                       |
	| Complex.Complex.Complex.Id | 5311                      |
	| Complex.String             | "Some string."            |
	| Complex.Integer            | 42                        |
	| Complex.Decimal            | 3.141592                  |
	| Complex.Boolean            | True                      |
	| Complex.DateTime           | "2020-02-20T12:34:56.789" |
	When I request a complex instance
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
Scenario: With multi-dimensional arrays
	Given I define a table like
	| Field          | Value |
	| Id             | 6     |
	| Items[0][0][0] | 1     |
	| Items[0][0][1] | 2     |
	| Items[0][0][2] | 3     |
	| Items[0][1][0] | 4     |
	| Items[0][1][1] | 5     |
	| Items[0][1][2] | 6     |
	| Items[1][0][0] | 7     |
	| Items[1][0][1] | 8     |
	| Items[1][0][2] | 9     |
	| Items[1][1][0] | 10    |
	| Items[1][1][1] | 11    |
	| Items[1][1][2] | 12    |
	| Items[1][2][0] | 13    |
	| Items[1][2][1] | 14    |
	| Items[1][2][2] | 15    |
	| Items[2][0][0] | 16    |
	| Items[2][0][1] | 17    |
	| Items[2][0][2] | 18    |
	| Items[2][1][0] | 19    |
	| Items[2][1][1] | 20    |
	| Items[2][1][2] | 21    |
	| Items[2][2][0] | 22    |
	| Items[2][2][1] | 23    |
	| Items[2][2][2] | 24    |
	| Items[3][0][0] | 25    |
	| Items[3][0][1] | 26    |
	| Items[3][0][2] | 27    |
	When I request a complex instance
	Then the result object should not be null
	And the 'Id' property should be '6'
	And the 'Items' property should have 4 items
	And the item 0 of the 'Items' property should have 2 items
	And the item 0, 0 of the 'Items' property should have 3 items
	And the item 0, 0, 0 of the 'Items' property should be 1
	And the item 0, 0, 1 of the 'Items' property should be 2
	And the item 0, 0, 2 of the 'Items' property should be 3
	And the item 0, 1 of the 'Items' property should have 3 items
	And the item 0, 1, 0 of the 'Items' property should be 4
	And the item 0, 1, 1 of the 'Items' property should be 5
	And the item 0, 1, 2 of the 'Items' property should be 6
	And the item 1 of the 'Items' property should have 3 items
	And the item 1, 0 of the 'Items' property should have 3 items
	And the item 1, 0, 0 of the 'Items' property should be 7
	And the item 1, 0, 1 of the 'Items' property should be 8
	And the item 1, 0, 2 of the 'Items' property should be 9
	And the item 1, 1 of the 'Items' property should have 3 items
	And the item 1, 1, 0 of the 'Items' property should be 10
	And the item 1, 1, 1 of the 'Items' property should be 11
	And the item 1, 1, 2 of the 'Items' property should be 12
	And the item 1, 2 of the 'Items' property should have 3 items
	And the item 1, 2, 0 of the 'Items' property should be 13
	And the item 1, 2, 1 of the 'Items' property should be 14
	And the item 1, 2, 2 of the 'Items' property should be 15
	And the item 2 of the 'Items' property should have 3 items
	And the item 2, 0 of the 'Items' property should have 3 items
	And the item 2, 0, 0 of the 'Items' property should be 16
	And the item 2, 0, 1 of the 'Items' property should be 17
	And the item 2, 0, 2 of the 'Items' property should be 18
	And the item 2, 1 of the 'Items' property should have 3 items
	And the item 2, 1, 0 of the 'Items' property should be 19
	And the item 2, 1, 1 of the 'Items' property should be 20
	And the item 2, 1, 2 of the 'Items' property should be 21
	And the item 2, 2 of the 'Items' property should have 3 items
	And the item 2, 2, 0 of the 'Items' property should be 22
	And the item 2, 2, 1 of the 'Items' property should be 23
	And the item 2, 2, 2 of the 'Items' property should be 24
	And the item 3 of the 'Items' property should have 1 items
	And the item 3, 0 of the 'Items' property should have 3 items
	And the item 3, 0, 0 of the 'Items' property should be 25
	And the item 3, 0, 1 of the 'Items' property should be 26
	And the item 3, 0, 2 of the 'Items' property should be 27

@Deserializer
Scenario: With dictionary property
	Given I define a table like
	| Field               | Value   |
	| Id                  | 7       |
	| Dictionary.Father   | "John"  |
	| Dictionary.Mother   | "Ana"   |
	| Dictionary.Son      | "Billy" |
	| Dictionary.Daughter | "Cindy" |
	When I request a complex instance
	Then the result object should not be null
	And the 'Id' property should be '7'
	And the 'Dictionary' property should have 4 items
	And the 'Father' key from the 'Dictionary' property should be 'John'
	And the 'Mother' key from the 'Dictionary' property should be 'Ana'
	And the 'Son' key from the 'Dictionary' property should be 'Billy'
	And the 'Daughter' key from the 'Dictionary' property should be 'Cindy'

@Deserializer
Scenario: With very complex property
	Given I define a table like
	| Field             | Value |
	| Id                | 9     |
	| Crazy[0].Red.Id   | 901   |
	| Crazy[0].Green.Id | 902   |
	| Crazy[1].Blue.Id  | 911   |
	| Crazy[1].White.Id | 912   |
	| Crazy[1].Black.Id | 913   |
	When I request a complex instance
	Then the result object should not be null
	And the 'Id' property should be '9'
	And the 'Crazy' property should have 2 items
	And the item 0 of the 'Crazy' property should have 2 items
	And the item 1 of the 'Crazy' property should have 3 items
	And the 'Id' property of the 'Red' key of the item 0 of the 'Crazy' property should be '901'
	And the 'Id' property of the 'Green' key of the item 0 of the 'Crazy' property should be '902'
	And the 'Id' property of the 'Blue' key of the item 1 of the 'Crazy' property should be '911'
	And the 'Id' property of the 'White' key of the item 1 of the 'Crazy' property should be '912'
	And the 'Id' property of the 'Black' key of the item 1 of the 'Crazy' property should be '913'

@Deserializer
Scenario: With tuple property
	Given I define a table like
	| Field             | Value   |
	| Id                | 8       |
	| SimpleTuple.Item1 | "Smith" |
	| SimpleTuple.Item2 | 7       |
	| SimpleTuple.Item3 | False   |
	| NamedTuple.Item1  | "Neo"   |
	| NamedTuple.Item2  | 42      |
	When I request a complex instance
	Then the result object should not be null
	And the 'Id' property should be '8'
	And the 'Item1' key from the 'SimpleTuple' property should be 'Smith'
	And the 'Item2' key from the 'SimpleTuple' property should be '7'
	And the 'Item3' key from the 'SimpleTuple' property should be 'False'
	And the 'Name' key from the 'NamedTuple' property should be 'Neo'
	And the 'Power' key from the 'NamedTuple' property should be '42'

@Deserializer
Scenario: Property value must be of a valid type
	Given I define a table like
	| Field | Value                |
	| Id    | Invalid Value Format |
	When I request a complex instance with an error
	Then it should throw 'JsonException' with message "The JSON value could not be converted to System.Int32. Path: $.Id | LineNumber: 0 | BytePositionInLine: 28."

@Deserializer
Scenario: Collection property index must be an number
	Given I define a table like
	| Field      | Value        |
	| Id         | 99           |
	| Lines[abc] | "Some line." |
	When I request a complex instance with an error
	Then it should throw 'InvalidDataException' with message "Invalid array index at 'Lines[abc]'."

@Deserializer
Scenario: Collection property index must start at 0
	Given I define a table like
	| Field    | Value        |
	| Id       | 99           |
	| Lines[1] | "Some line." |
	When I request a complex instance with an error
	Then it should throw 'InvalidDataException' with message "Invalid array index at 'Lines[1]'."

@Deserializer
Scenario: Collection property index must not be negative
	Given I define a table like
	| Field     | Value        |
	| Id        | 99           |
	| Lines[-1] | "Some line." |
	When I request a complex instance with an error
	Then it should throw 'InvalidDataException' with message "Invalid array index at 'Lines[-1]'."

@Deserializer
Scenario: Collection property index must be in sequence
	Given I define a table like
	| Field    | Value              |
	| Id       | 99                 |
	| Lines[0] | "Some line."       |
	| Lines[2] | "Some other line." |
	When I request a complex instance with an error
	Then it should throw 'InvalidDataException' with message "Invalid array index at 'Lines[2]'."

@Deserializer
Scenario: Collection property index cannot repeat
	Given I define a table like
	| Field    | Value              |
	| Id       | 99                 |
	| Lines[0] | "Some line."       |
	| Lines[0] | "Some other line." |
	When I request a complex instance with an error
	Then it should throw 'InvalidDataException' with message "Invalid array index at 'Lines[0]'."
