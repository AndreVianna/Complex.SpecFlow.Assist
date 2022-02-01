Feature: Deserializer
Transforms table in complex types

@Deserializer
Scenario: Complex class with vertical table minimal required fields
	When I define a table like
	| Field | Value                                  |
	| Id    | "CEFFAB59-D548-4E3A-B05E-5FDA43B4A4CF" |
	Then the result object should not be null
	And the 'Id' property should be 'CEFFAB59-D548-4E3A-B05E-5FDA43B4A4CF'

Scenario: Complex class with vertical table all basic fields
	When I define a table like
	| Field    | Value                                  |
	| Id       | "1f576fa6-16c9-4905-95f8-e00cad6a8ded" |
	| String   | "Some string."                         |
	| Integer  | 42                                     |
	| Decimal  | 3.141592                               |
	| Boolean  | True                                   |
	| DateTime | "2020-02-20T12:34:56.789"              |
	Then the result object should not be null
	And the 'Id' property should be '1f576fa6-16c9-4905-95f8-e00cad6a8ded'
	And the 'String' property should be 'Some string.'
	And the 'Integer' property should be '42'
	And the 'Decimal' property should be '3.141592'
	And the 'Boolena' property should be 'True'
	And the 'DateTime' property should be '2020-02-20T12:34:56.789'
