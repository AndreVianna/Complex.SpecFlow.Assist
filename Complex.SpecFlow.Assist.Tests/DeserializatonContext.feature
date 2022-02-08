Feature: Deserialization context
Use a deserialization context to store pre-defined objects and use during the deserialization process

@Deserializer
Scenario: Using vertical table get an intance from a stored object
	Given I define a table like
	| Field | Value |
	| Id    | 1     |
	And store as an instance in a context under 'StoredObject'
	And I define a table like
	| Field   | Value          |
	| Id      | 2              |
	| Complex | {StoredObject} |
	When I request a complex instance with a context
	Then the result object should not be null
	And the result object should be
	| Field      | Value |
	| Id         | 2     |
	| Complex.Id | 1     |

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
	And store as a set of strings in a context under 'Guids'
	And I define a table like
	| Id  | Guid      |
	| 101 | {Guids:3} |
	| 201 | {Guids:2} |
	| 301 | {Guids:1} |
	| 401 | {Guids:0} |
	When I request a complex set with a context
	Then the result collection should have 4 items
	And the 'Id' property of the item 0 should be '101'
	And the 'Guid' property of the item 0 should be 'B7B95D5B-CE20-4FE4-987D-694511AF880C'
	And the 'Id' property of the item 2 should be '301'
	And the 'Guid' property of the item 2 should be '3346B887-5219-43DE-980D-213985D33847'

@Deserializer
Scenario: Using vertical table get single item form a stored collection
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field   | Value           |
	| Id      | 2               |
	| Complex | {StoredArray:2} |
	When I request a complex instance with a context
	Then the result object should not be null
	And the result object should be
	| Field      | Value |
	| Id         | 2     |
	| Complex.Id | 30    |

@Deserializer
Scenario: Using vertical table get the whole list from a stored collection
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field    | Value         |
	| Id       | 2             |
	| Children | [StoredArray] |
	When I request a complex instance with a context
	Then the result object should not be null
	And the result object should be
	| Field          | Value |
	| Id             | 2     |
	| Children[0].Id | 10    |
	| Children[1].Id | 20    |
	| Children[2].Id | 30    |
	| Children[3].Id | 40    |

@Deserializer
Scenario: Using vertical table get selected items from a stored collection
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field    | Value               |
	| Id       | 2                   |
	| Children | [StoredArray:1,3,2] |
	When I request a complex instance with a context
	Then the result object should not be null
	And the result object should be
	| Field          | Value |
	| Id             | 2     |
	| Children[0].Id | 20    |
	| Children[1].Id | 40    |
	| Children[2].Id | 30    |

@Deserializer
Scenario: Using vertical table get an instance from a stored instance as a collection
	Given I define a table like
	| Field | Value |
	| Id    | 1     |
	And store as an instance in a context under 'StoredObject'
	And I define a table like
	| Field    | Value          |
	| Id       | 2              |
	| Children | [StoredObject] |
	When I request a complex instance with a context
	Then the result object should not be null
	And the result object should be
	| Field          | Value |
	| Id             | 2     |
	| Children[0].Id | 1     |

@Deserializer
Scenario: Use previous values of the table definition
	Given I define a table like
	| Id | Complex        | Integer   | Children     |
	| 10 |                | {_index_} |              |
	| 20 | {_previous_:0} | {_index_} |              |
	| 30 | {_previous_:0} | {_index_} |              |
	| 40 | {_previous_:1} | {_index_} |              |
	| 50 |                | {_index_} | [_previous_] |
	When I request a complex set
	Then the result collection should have 5 items
	And the 'Id' property of the item 0 should be '10'
	And the 'Integer' property of the item 0 should be '0'
	And the 'Id' property of the item 1 should be '20'
	And the 'Integer' property of the item 1 should be '1'
	And the 'Complex.Id' property of the item 1 should be '10'
	And the 'Id' property of the item 2 should be '30'
	And the 'Integer' property of the item 2 should be '2'
	And the 'Complex.Id' property of the item 2 should be '10'
	And the 'Id' property of the item 3 should be '40'
	And the 'Integer' property of the item 3 should be '3'
	And the 'Complex.Id' property of the item 3 should be '20'
	And the 'Complex.Complext.Id' property of the item 3 should be '10'
	And the 'Id' property of the item 4 should be '50'
	And the 'Integer' property of the item 4 should be '4'
	And the 'Children' property of the item 4 should have 4 items

@Deserializer
Scenario: Try to get a key not stored in the context when returning an instance
	And I define a table like
	| Field   | Value     |
	| Id      | 2         |
	| Complex | {Invalid} |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "The key 'Invalid' was not found in the deserialization context at 'Complex'."

@Deserializer
Scenario: Try to use an index with an object stored as an instance
	Given I define a table like
	| Field | Value |
	| Id    | 1     |
	And store as an instance in a context under 'StoredObject'
	And I define a table like
	| Field   | Value            |
	| Id      | 2                |
	| Complex | {StoredObject:0} |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "The key 'StoredObject' of the deserialization context contains a single object and can't be used with an index at 'Complex'."

@Deserializer
Scenario: Try to use more than one index when returning an instance
	Given I define a table like
	| Field | Value |
	| Id    | 1     |
	And store as an instance in a context under 'StoredObject'
	And I define a table like
	| Field   | Value              |
	| Id      | 2                  |
	| Complex | {StoredObject:1,2} |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "An instance value can not have more than one index at 'Complex'."

@Deserializer
Scenario: Try to assign a collection when returning an instance
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field   | Value         |
	| Id      | 2             |
	| Complex | {StoredArray} |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "The key 'StoredArray' of the deserialization context contains a collection and can't be assigned to an instance without an index at 'Complex'."

@Deserializer
Scenario: Try to apply a non-numeric index to stored collection when returning an instance
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field   | Value           |
	| Id      | 2               |
	| Complex | {StoredArray:x} |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "'x' is not a valid index for the collection contained in the deserialization context under 'StoredArray' at 'Complex'."

@Deserializer
Scenario: Try to apply a negative index to stored collection when returning an instance
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field   | Value            |
	| Id      | 2                |
	| Complex | {StoredArray:-1} |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "'-1' is not a valid index for the collection contained in the deserialization context under 'StoredArray' at 'Complex'."

@Deserializer
Scenario: Try to apply an index not contained in the stored collection when returning an instance
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field   | Value           |
	| Id      | 2               |
	| Complex | {StoredArray:4} |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "'4' is not a valid index for the collection contained in the deserialization context under 'StoredArray' at 'Complex'."

@Deserializer
Scenario: Try to get a key not stored in the context when returning a collection
	And I define a table like
	| Field    | Value     |
	| Id       | 2         |
	| Children | [Invalid] |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "The key 'Invalid' was not found in the deserialization context at 'Children'."

@Deserializer
Scenario: Try to apply an index to a stored instance when returning a collection
	Given I define a table like
	| Field | Value |
	| Id    | 1     |
	And store as an instance in a context under 'StoredObject'
	And I define a table like
	| Field    | Value            |
	| Id       | 2                |
	| Children | [StoredObject:1] |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "The key 'StoredObject' of the deserialization context does not contain a collection at 'Children'."

@Deserializer
Scenario: Try to apply a non-numeric index to stored collection when returning a collection
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field    | Value             |
	| Id       | 2                 |
	| Children | [StoredArray:x,2] |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "'x,2' is not a valid set of indexes for the collection contained in the deserialization context under 'StoredArray' at 'Children'."

@Deserializer
Scenario: Try to apply a negative index to stored collection when returning a collection
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field    | Value              |
	| Id       | 2                  |
	| Children | [StoredArray:-1,2] |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "'-1,2' is not a valid set of indexes for the collection contained in the deserialization context under 'StoredArray' at 'Children'."

@Deserializer
Scenario: Try to apply an index not contained in the stored collection when returning a collection
	Given I define a table like
	| Id |
	| 10 |
	| 20 |
	| 30 |
	| 40 |
	And store as a set in a context under 'StoredArray'
	And I define a table like
	| Field    | Value             |
	| Id       | 2                 |
	| Children | [StoredArray:4,5] |
	When I request a complex instance with a context with an error
	Then it should throw 'InvalidDataException' with message "'4,5' is not a valid set of indexes for the collection contained in the deserialization context under 'StoredArray' at 'Children'."

