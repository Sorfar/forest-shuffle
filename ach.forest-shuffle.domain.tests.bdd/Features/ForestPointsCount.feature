Feature: ForestPointsCount

a 'short summary of the feature

@tag1
Scenario: Test calcul de points simple
	Given a plot with a 'silver fir' tree as habitat
	And a 'purple emperor'' on the top
	And a 'wild boar' on the right
	And a 'fallow deer' on the left
	And a plot with an 'oak' tree as habitat
	And a 'mountain hare' on the left
	And an 'edelweiss' on the bottom
	When I get the total points of the forest
	Then the number result should be 16

Scenario: Calcul de points complexe orienté chauve souris
	Given a plot with a 'sapin douglas' tree as habitat
	And a 'tipule' on the left
	And an 'oreillard roux' of type beech on the right
	And a 'petit apollon' on the top

	And a plot with a 'bouleau' tree as habitat
	And a 'murin de bechstein' on the left
	And a 'vespère de savi' on the right
	And a 'rainette verte' on the bottom
	And an 'effraie des clochers' on the top

	And a plot with a 'tilleul' tree as habitat
	And a 'barbastelle d'europe' on the left
	And a 'grand rhinolophe' on the right
	And a 'grande tortue' on the top
	And a 'hérisson commun' on the bottom

	And a plot with a 'hêtre' tree as habitat
	And a 'pipistrelle commune' on the right
	And a 'pipistrelle commune' on the left
	And a 'carte géographique' on the top

	And a plot with a 'bouleau' tree as habitat

	And a plot with a 'érable' tree as habitat

	And a plot with a 'érable' tree as habitat
	And a 'paon-du-jour' on the top

	And a plot with a 'hêtre' tree as habitat
	And a 'tabac d'espagne' on the top 
	And a 'chevreuil' of type beech on the right

	And a plot with a 'hêtre' tree as habitat
	And a 'moustique' on the left
	And a 'moustique' on the right
	And a 'geai des chênes' on the top

	And a plot with a 'bouleau' tree as habitat
	And a 'bouquetin des alpes' on the left
	And a 'moustique' on the right
	When I get the total points of the forest
	Then the number result should be 188

Scenario: Calcul de points complexe orienté insectes
	Given a plot with a 'hêtre' tree as habitat
	And a 'fouine' on the left
	And a 'fouine' on the right
	And a 'girolle' on the bottom
	And a 'pic épeiche' on the top
	And a plot with a 'marronnier commun' tree as habitat
	And a 'lynx' on the left
	And a 'fouine' of type HorseChestnut on the right
	And a 'morio' on the top
	And 'mousse' on the bottom
	And a plot with a 'marronnier commun' tree as habitat
	And a 'lynx' on the left
	And a 'chevreuil' of type HorseChestnut on the right
	And a 'grand mars changeant' on the top
	And a 'fougère arborescente' on the bottom
	And a plot with a 'marronnier commun' tree as habitat
	And a 'grande tortue' on the top
	And a 'marcassin' of type HorseChestnut on the left
	And a 'bouquetin des alpes' on the right
	And a 'taupe' on the bottom
	And a plot with a 'hêtre' tree as habitat
	And a plot with a 'marronnier commun' tree as habitat
	And a 'bouquetin des alpes' on the right
	And a 'petit apollon' on the top
	And a 'salamandre tachetée' on the bottom
	And a plot with a 'mélèze d'europe' tree as habitat
	And a 'tabac d'espagne' on the top
	And a 'gentiane' on the bottom
	And a plot with a 'hêtre' tree as habitat
	And a 'triton alpestre' on the bottom
	And a plot with a 'marronnier commun' tree as habitat
	And a 'cistude' on the bottom
	And a plot with a 'hêtre' tree as habitat
	And a 'carte géographique' on the top
	And a 'fougère arborescente' of type HorseChestnut on the bottom
	And a plot with a 'marronnier commun' tree as habitat
	And a 'crapaud commun' on the bottom
	When I get the total points of the forest
	Then the number result should be 333