Feature: Horizontal deserializer
Transforms a horizontal table in a collection of complex object

@Deserializer
Scenario: Using horizontal table
	Given I define a table like
	| Id | String           | Integer | Decimal  | Boolean | DateTime                  | Guid                                   |
	| 1  | Some string.     | 42      | 3.141592 | True    | '2020-02-20T12:34:56.789' | "8D8CDA94-861B-42E4-9836-D972E2F3235B" |
	| 2  | Another string.  | 43      | 6.283185 | True    | '2020-02-21T12:35:00.000' | "72346CA1-B732-4674-9685-491A1C9020D0" |
	| 3  | One more string. | 44      | 2.718282 | False   | '2020-02-22T23:55:55.111' | "0562F255-A838-415F-9082-3D4A00F06C07" |
	When I request a complex set
	Then the result collection should have 3 items
	And the 'Id' property of the item 0 should be '1'

@Deserializer
Scenario: One line with a invalid property value
	Given I define a table like
	| Id      |
	| 1       |
	| Invalid |
	| 3       |
	When I request a complex set with an error
	Then it should throw 'InvalidOperationException' with message "An error has occurred while deserializing line 1."
