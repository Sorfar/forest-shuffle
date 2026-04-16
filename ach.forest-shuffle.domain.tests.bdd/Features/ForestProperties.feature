Feature: ForestProperties

Tests of the properties of a forest

Scenario: Get number of birds
	Given a plot with an oak tree as habitat
	And an eurasian jay on the top
	When I get the number of living organism of type icon 'bird'
	Then the number result should be 1

Scenario: Get number of oaks
	Given a plot with an oak tree as habitat
	And a plot with a birch tree as habitat
	And a plot with a oak tree as habitat
	And a plot with a sapling as habitat
	When I get the number of trees of type oak
	Then the number result should be 2

Scenario: Get number of oaks integrating violet carpenter bees
	Given a plot with an oak tree as habitat
	And a violet carpenter bee on the left
	And a plot with a birch tree as habitat
	And a violet carpenter bee on the right
	And a plot with a oak tree as habitat
	And a plot with a sapling as habitat
	When I get the number of trees of type oak
	Then the number result should be 3

Scenario: Get distinct number of plants
	Given a plot with a sapling as habitat
	And blackberries on the bottom
	And a plot with a sapling as habitat
	And blackberries on the bottom
	And a plot with a sapling as habitat
	And moss on the bottom
	And a plot with a sapling as habitat
	And wild strawberries on the bottom
	When I get the distinct number of living organism of type icon 'plant'
	Then the number result should be 3