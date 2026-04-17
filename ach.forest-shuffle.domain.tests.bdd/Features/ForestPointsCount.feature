Feature: ForestPointsCount

A short summary of the feature

@tag1
Scenario: Test calcul de points simple
	Given a plot with a silver fir tree as habitat
	And a purple emperor on the top
	And a wild boar on the right
	And a fallow deer on the left
	And a plot with an oak tree as habitat
	And a mountain hare on the left
	And edelweiss on the bottom
	When I get the total points of the forest
	Then the number result should be 16
