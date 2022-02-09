Feature: Horizontal deserializer
Transforms a horizontal table in a collection of complex object

@Deserializer
Scenario: Using horizontal table for primitive types
	Given I define a table like
	| {self}                               |
	| E7BD910E-B939-4711-978E-C6D81AC037D8 |
	| 3346B887-5219-43DE-980D-213985D33847 |
	| CADA29E3-A126-48F7-81BD-F07083773A6A |
	| B7B95D5B-CE20-4FE4-987D-694511AF880C |
	| 5E882615-E83A-45BD-A950-AD61633BEE9A |
	| BE61A824-52B0-42BC-978B-A23AF10B06EB |
	| 30D73007-9D42-48B8-8D09-E67485EC01D7 |
	When I request a set of strings
	Then the result collection of strings should have 7 items
	And the item 0 should be 'E7BD910E-B939-4711-978E-C6D81AC037D8'
	And the item 3 should be 'B7B95D5B-CE20-4FE4-987D-694511AF880C'

@Deserializer
Scenario: Using horizontal table for complex types
	Given I define a table like
	| Id | String           | Integer | Decimal  | Boolean | DateTime                  | Guid                                   |
	| 1  | Some string.     | 42      | 3.141592 | True    | '2020-02-20T12:34:56.789' | "8D8CDA94-861B-42E4-9836-D972E2F3235B" |
	| 2  | Another string.  | 43      | 6.283185 | True    | '2020-02-21T12:35:00.000' | "72346CA1-B732-4674-9685-491A1C9020D0" |
	| 3  | One more string. | 44      | 2.718282 | False   | '2020-02-22T23:55:55.111' | "0562F255-A838-415F-9082-3D4A00F06C07" |
	When I request a complex set
	Then the result collection should have 3 items
	And the 'Id' property of the item 0 should be '1'
	And the 'Id' property of the item 2 should be '3'

@Deserializer
Scenario: With a onCreated delegate
	Given I define a table like
	| Id | !Extra |
	| 1  | Pi     |
	| 2  |        |
	| 3  | Tau    |
	When I request a complex set with a onCreated delegate
	Then the result collection should have 3 items
	And the 'Id' property of the item 0 should be '1'
	And the 'String' property of the item 0 should be 'Set during config at index 0.'
	And the 'Decimal' property of the item 0 should be '3.141592'
	And the 'Id' property of the item 1 should be '2'
	And the 'String' property of the item 1 should be 'Set during config at index 1.'
	And the 'Decimal' property of the item 1 should be null
	And the 'Id' property of the item 2 should be '3'
	And the 'String' property of the item 2 should be 'Set during config at index 2.'
	And the 'Decimal' property of the item 2 should be '6.283185'

@Deserializer
Scenario: One line with a invalid property value
	Given I define a table like
	| Id      |
	| 1       |
	| Invalid |
	| 3       |
	When I request a complex set with an error
	Then it should throw 'InvalidOperationException' with message "An error has occurred while deserializing line 1."
	And the inner exception should be 'InvalidCastException' with message "The value at 'Id' is not of the correct type."

@Deserializer
Scenario: Collection property index must not be negative
	Given I define a table like
	| Id | Lines[-1]    |
	| 1  | "Some line"  |
	| 2  | "Other line" |
	When I request a complex set with an error
	Then it should throw 'InvalidOperationException' with message "An error has occurred while deserializing line 0."
	And the inner exception should be 'InvalidDataException' with message "Invalid array index at 'Lines[-1]'."
