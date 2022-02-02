Feature: Deserializer
Transforms table in complex types

@Deserializer
Scenario: Complex class with vertical table
	When I define a table like
	| Field | Value                                  |
	| Id    | "CEFFAB59-D548-4E3A-B05E-5FDA43B4A4CF" |
	Then the result object should not be null
	And the 'Id' property should be 'CEFFAB59-D548-4E3A-B05E-5FDA43B4A4CF'

Scenario: Complex class with basic properties
	When I define a table like
	| Field    | Value                                  |
	| Id       | "1f576fa6-16c9-4905-95f8-e00cad6a8ded" |
	| String   | "Some string."                         |
	| Integer  | 42                                     |
	| Decimal  | 3.141592                               |
	| Boolean  | True                                   |
	| DateTime | "2020-02-20T12:34:56.789"              |
	Then the result object should not be null
	And the 'Id' property should be '1F576FA6-16C9-4905-95F8-E00CAD6A8DED'
	And the 'String' property should be 'Some string.'
	And the 'Integer' property should be '42'
	And the 'Decimal' property should be '3.141592'
	And the 'Boolean' property should be 'True'
	And the 'DateTime' property should be '2020-02-20T12:34:56.789'

Scenario: Complex class with nullable properties
	When I define a table like
	| Field    | Value                                  |
	| Id       | "E01366F0-95FA-4372-AB7F-89F9DFBEF501" |
	| String   | null                                   |
	| Integer  | NULL                                   |
	| Decimal  | Null                                   |
	| Boolean  | default                                |
	| DateTime |                                        |
	| Complex  | null                                   |
	Then the result object should not be null
	And the 'Id' property should be 'E01366F0-95FA-4372-AB7F-89F9DFBEF501'
	And the 'String' property should be null
	And the 'Integer' property should be null
	And the 'Decimal' property should be null
	And the 'Boolean' property should be null
	And the 'DateTime' property should be null
	And the 'Complex' property should be null

Scenario: Complex class with collection properties
	When I define a table like
	| Field      | Value                                  |
	| Id         | "92849268-2695-4726-AD10-486BD407BE64" |
	| Lines[0]   | "Some line."                           |
	| Lines[1]   | ""                                     |
	| Lines[2]   | "Another line."                        |
	| Lines[3]   | "Last line."                           |
	| Numbers[0] | 101                                    |
	| Numbers[1] | -201                                   |
	| Numbers[2] | 0                                      |
	Then the result object should not be null
	And the 'Id' property should be '92849268-2695-4726-AD10-486BD407BE64'
	And the 'Lines' property should have 4 items
	And the item 0 from 'Lines' should be 'Some line.'
	And the item 1 from 'Lines' should be ''
	And the item 2 from 'Lines' should be 'Another line.'
	And the item 3 from 'Lines' should be 'Last line.'
	And the 'Numbers' property should have 3 items
	And the item 0 from 'Numbers' should be '101'
	And the item 1 from 'Numbers' should be '-201'
	And the item 2 from 'Numbers' should be '0'

Scenario: Complex class with complex properties
	When I define a table like
	| Field                      | Value                                  |
	| Id                         | "7AA4F67D-52A4-42FF-B4DE-57C50063314B" |
	| Children[0].Id             | "9C4060F9-F949-4036-B82C-E16E7413351D" |
	| Children[0].String         | "Some string."                         |
	| Children[0].Integer        | 42                                     |
	| Children[1].Id             | "C052E9F2-BA16-41A0-A30C-6A4B0DF9AF33" |
	| Children[1].Decimal        | 3.141592                               |
	| Children[1].Boolean        | False                                  |
	| Children[1].DateTime       | "2020-02-20T12:34:56.789"              |
	| Complex.Id                 | "782BCD4A-7BD2-455F-9841-52F368C8BAA1" |
	| Complex.String             | "Some string."                         |
	| Complex.Integer            | 42                                     |
	| Complex.Decimal            | 3.141592                               |
	| Complex.Boolean            | True                                   |
	| Complex.DateTime           | "2020-02-20T12:34:56.789"              |
	| Complex.Complex.Id         | "2EDD7F54-9B0F-4F68-B090-41CE9F9D8402" |
	| Complex.Complex.Complex.Id | "201C8E6D-9AFE-4244-A21F-288BD1C0460B" |
	Then the result object should not be null
	And the 'Id' property should be '7AA4F67D-52A4-42FF-B4DE-57C50063314B'
	And the 'Children' property should have 2 items
	And the item 0 from 'Children' should be
	| Field   | Value                                  |
	| Id      | "9C4060F9-F949-4036-B82C-E16E7413351D" |
	| String  | "Some string."                         |
	| Integer | 42                                     |
	And the item 1 from 'Children' should be
	| Field    | Value                                  |
	| Id       | "C052E9F2-BA16-41A0-A30C-6A4B0DF9AF33" |
	| Decimal  | 3.141592                               |
	| Boolean  | False                                  |
	| DateTime | "2020-02-20T12:34:56.789"              |
	And the 'Complex' property should be
	| Field              | Value                                  |
	| Id                 | "782BCD4A-7BD2-455F-9841-52F368C8BAA1" |
	| String             | "Some string."                         |
	| Integer            | 42                                     |
	| Decimal            | 3.141592                               |
	| Boolean            | True                                   |
	| DateTime           | "2020-02-20T12:34:56.789"              |
	| Complex.Id         | "2EDD7F54-9B0F-4F68-B090-41CE9F9D8402" |
	| Complex.Complex.Id | "201C8E6D-9AFE-4244-A21F-288BD1C0460B" |

Scenario: Invalid property value
	When I define an invalid table like
	| Field | Value                   |
	| Id    | {Invalide Value Format} |
	Then the process should throw InvalidCastException for property 'Id'
