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

Scenario: Calcul de points complexe orienté chauve souris
	Given a plot with a sapin douglas tree as habitat
	And a tipule on the left
	And an oreillard roux on the right
	And a petit apollon on the top
	And a plot with a bouleau tree as habitat
	And a murin de bechstein on the left
	And a vespère de savi on the right
	And a rainette verte on the bottom
	And an effraie des clochers on the top
	And a plot with a tilleul tree as habitat
	And a barbastelle d'europe on the left
	And a grand rhinolophe on the right
	And a grande tortue on the top
	And a hérisson commun on the bottom
	And a plot with a hêtre tree as habitat
	And a pipistrelle commune on the right
	And a pipistrelle commune on the left
	And a carte géographique on the top
	And a plot with a bouleau tree as habitat
	And a plot with a érable tree as habitat
	And a paon-du-jour on the top
	And plot with a hêtre tree as habitat
	And a tabac d'espagne on the top 
	And a chevreuil on the right
	When I get the total points of the forest
	Then the number result should be 20