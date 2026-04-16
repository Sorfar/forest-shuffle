namespace ach.forest_shuffle.domain.Forest;

internal class ForestAggregate
{
    private ForestAggregate()
    {
    }

    private List<Plot> _plots;

    public int Cave { get; private set; }

    public IReadOnlyList<Biota> Biota => _plots.SelectMany(p => p.Biota).ToList();

    public IReadOnlyList<Plot> Plots => _plots;

    public static ForestAggregate Create()
    {
        return new ForestAggregate();
    }

    public int NumberOf(TypeIcon typeIcon)
    {
        return Biota.Count(b => b.HasType(typeIcon));
    }

    public int NumberOf(TreeIcon treeIcon)
    {
        return Biota.Count(b => b.HasType(treeIcon));
    }

    public int DistinctNumberOf(TypeIcon typeIcon)
    {
        return Biota.Where(b => b.HasType(typeIcon)).Select(b => b.GetType()).Distinct().Count();
    }

    public int DistinctNumberOfTrees()
    {
        return Biota.Where(b => b.HasType(TypeIcon.Tree) && b.TreeIcon is not TreeIcon.None).Select(b => b.GetType()).Distinct().Count();
    }

    public int NumberOfTree()
    {
        return Biota.Count(b => b is Tree) + NumberOfCarpenterBees();
    }

    public int NumberOfTree(Tree tree)
    {
        return Biota.Count(b => b.GetType() == tree.GetType()) + NumberOfCarpenterBees(tree);
    }

    public int NumberOfCarpenterBees()
    {
        return Plots.Where(p => p.Mainstay is Tree).Sum(p => p.Biota.Count(b => b is VioletCarpenterBee));
    }

    public int NumberOfCarpenterBees(Tree tree)
    {
        return Plots.Where(p => p.Mainstay.GetType() == tree.GetType()).Sum(p => p.Biota.Count(b => b is VioletCarpenterBee));
    }
}

internal class Plot
{
    private Plot()
    {

    }

    public Mainstay Mainstay { get; private set; }

    public List<Dweller> TopDwellers { get; private set; }

    public List<Dweller> BottomDwellers { get; private set; }

    public List<Dweller> LeftDwellers { get; private set; }

    public List<Dweller> RightDwellers { get; private set; }

    public IReadOnlyList<Biota> Biota => [Mainstay, ..TopDwellers, ..BottomDwellers, ..LeftDwellers, ..RightDwellers];

    public IReadOnlyList<Biota> Dwellers => [.. TopDwellers, .. BottomDwellers, .. LeftDwellers, .. RightDwellers];

    public bool FullyOccupied => Biota.All(b => b != null);
}

internal abstract class Mainstay(TreeIcon treeIcon) : Biota(treeIcon)
{
}

internal abstract class Tree(TreeIcon treeIcon) : Mainstay(treeIcon)
{
}

internal abstract class Shrub() : Mainstay(TreeIcon.None)
{
}

internal abstract class Dweller(TreeIcon treeIcon) : Biota(treeIcon)
{
}

internal abstract class Butterfly(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.DistinctNumberOf(TypeIcon.Butterfly) switch
        {
            2 => 1.5,
            3 => 2,
            4 => 3,
            5 => 4,
            6 => 5.833,
            7 => 7.857,
            >= 8 => 10,
            _ => 0,
        };
    }
}

internal abstract class Hare(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.Biota.Count(b => b.GetType() == typeof(Hare));
    }
}

internal abstract class Bat(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.DistinctNumberOf(TypeIcon.Bat) >= 3 ? 5 : 0;
    }
}

internal abstract class Biota(TreeIcon treeIcon)
{
    public TreeIcon TreeIcon { get; } = treeIcon;

    public abstract List<TypeIcon> TypeIcons { get; }

    public abstract double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot);

    public bool HasType(TypeIcon typeIcon)
    {
        return TypeIcons.Contains(typeIcon);
    }

    public bool HasType(TreeIcon treeIcon)
    {
        return TreeIcon == treeIcon;
    }
}

internal enum TreeIcon
{
    None,
    Sycamore,
    Birch,
    Beech,
    DouglasFir,
    Oak,
    HorseChestnut,
    Linden,
    SilverFir,
    EuropeanLarch,
    StonePine,
}

internal enum TypeIcon
{
    Amphibian,
    Tree,
    Bat,
    Deer,
    Insect,
    ClovenHoofedAnimal,
    Plant,
    PawedAnimal,
    Mushroom,
    Butterfly,
    Bird,
    Alpine,
    Shrub,
    WoodlandEdge,

}

internal static class TypeIconExtensions
{
    public static bool HasType(this Tree biota, TypeIcon typeIcon)
    {
        return biota.TypeIcons.Contains(typeIcon);
    }
}

internal class AlpineMarmot(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.DistinctNumberOf(TypeIcon.Plant);
    }
}

internal class AlpineNewt(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Amphibian, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Insect);
    }
}

internal class BarbastelleBat(TreeIcon treeIcon) : Bat(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bat];
}

internal class BarnOwl(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TypeIcon.Bat);
    }
}

internal class BeardedVulture(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.Cave;
    }
}

internal class BeeSwarm(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Plant);
    }
}

internal class BechsteinsBat(TreeIcon treeIcon) : Bat(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bat];
}

internal class Beech() : Tree(TreeIcon.Beech)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        var numberOfBeeches = forest.NumberOfTree(this);

        return forest.NumberOfTree(this) >= 4 ? 5 : 0;
    }
}

internal class BeechMarten(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5 * forest.Plots.Count(p => p.FullyOccupied);
    }
}

internal class Birch() : Tree(TreeIcon.Birch)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 1;
    }
}

internal class BlackTrumpet(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Mushroom, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class Blackthorn : Shrub
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Shrub, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class Blackberries(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Plant);
    }
}

internal class BlueBerry(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.DistinctNumberOf(TypeIcon.Bird);
    }
}

internal class BrownBear(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class BrownLongEaredBat(TreeIcon treeIcon) : Bat(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bat];
}

internal class Bullfinch(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Insect);
    }
}

internal class CamberwellBeauty(TreeIcon treeIcon) : Butterfly(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect, TypeIcon.Butterfly];
}

internal class Capercaillie(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Plant);
    }
}

internal class Chaffinch(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return plot.Mainstay is Beech ? 5 : 0;
    }
}

internal class Chamois(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TreeIcon);
    }
}


internal class Chanterelle(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Mushroom];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class CommonHazel : Shrub
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Shrub, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class CommonPipistrelle(TreeIcon treeIcon) : Bat(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bat, TypeIcon.WoodlandEdge];
}

internal class CommonRaven(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5;
    }
}


internal class CommonToad(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Amphibian];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return plot.BottomDwellers.Count(d => d.GetType() == typeof(CommonToad)) == 2 ? 5 : 0;
    }
}


internal class CraneFly(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Bat);
    }
}

internal class Digitalis(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.DistinctNumberOf(TypeIcon.Plant) switch
        {
            1 => 1,
            2 => 1.5,
            3 => 2,
            4 => 2.5,
            >= 5 => 3,
            _ => 0,
        };
    }
}

internal class DouglasFir(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5;
    }
}

internal class Edelweiss(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3;
    }
}

internal class Elderberry : Shrub
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Shrub, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class EurasianJay(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3;
    }
}

internal class EuropeanBadger(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2;
    }
}

internal class EuropeanBison(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TreeIcon.Beech) + 2 * forest.NumberOf(TreeIcon.Oak);
    }
}

internal class EuropeanFatDormouse(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];
    
    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return plot.Biota.Any(b => b.HasType(TypeIcon.Bat)) ? 15 : 0;
    }
}

internal class EuropeanHare(TreeIcon treeIcon) : Hare(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];
}

internal class EuropeanLarch() : Tree(TreeIcon.EuropeanLarch)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3;
    }
}

internal class EurasianMagpie(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3;
    }
}

internal class EuropeanPolecat(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return plot.Dwellers.Count == 1 ? 10 : 0;
    }
}

internal class EuropeanWildcat(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.WoodlandEdge);
    }
}

internal class FallowDeer(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Deer, TypeIcon.ClovenHoofedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TypeIcon.ClovenHoofedAnimal);
    }
}

internal class Fireflies(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        var fireFliesCount = forest.Biota.Count(b => b is Fireflies);
        return fireFliesCount >= 1 ? fireFliesCount * 5 : 0;
    }
}

internal class FireSalamander(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Amphibian];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.Biota.Count(b => b is FireSalamander) switch
        {
            1 => 5,
            2 => 7.5,
            >= 3 => 8.334,
            _ => 0
        };
    }
}

internal class FlyAgaric(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Mushroom];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class Gentian(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TypeIcon.Butterfly);
    }
}

internal class Gnat(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Bat);
    }
}

internal class GoldenEagle(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.PawedAnimal) + forest.NumberOf(TypeIcon.Amphibian);
    }
}

internal class Goshawk(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TypeIcon.Bird);
    }
}

internal class GreatGreenBushCricket(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Insect);
    }
}

internal class GreaterHorseshoeBat(TreeIcon treeIcon) : Bat(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bat];
}

internal class GreatSpottedWoodpecker(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return otherForests.All(of => of.NumberOfTree() < forest.NumberOfTree()) ? 10 : 0;
    }
}

internal class Hedgehog(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Butterfly);
    }
}

internal class HorseChestnut() : Tree(TreeIcon.HorseChestnut)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        var count = forest.NumberOfTree(this);
        return count >= 8 ? 7 : count;
    }
}

internal class LargeTortoiseshell(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect, TypeIcon.Butterfly];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class Linden() : Tree(TreeIcon.Linden)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return otherForests.All(of => of.NumberOfTree(this) < forest.NumberOfTree(this)) ? 3 : 1;
    }
}

internal class Lynx(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.Biota.Any(b => b is RoeDeer) ? 10 : 0;
    }
}

internal class MapButterfly(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Butterfly, TypeIcon.Insect, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class Mole(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class Moss(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOfTree() >= 10 ? 10 : 0;
    }
}

internal class MountainHare(TreeIcon treeIcon) : Hare(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.Alpine];
}

internal class Nightingale(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return plot.Mainstay is Shrub ? 5 : 0;
    }
}

internal class Oak() : Tree(TreeIcon.Oak)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.DistinctNumberOfTrees() >= 8 ? 10 : 0;
    }
}

internal class ParasolMushroom(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Mushroom];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class PeacockButterfly(TreeIcon treeIcon) : Butterfly(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Butterfly, TypeIcon.Insect];
}

internal class PennyBun(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Mushroom];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class PhoebusApollo(TreeIcon treeIcon) : Butterfly(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Butterfly, TypeIcon.Insect, TypeIcon.Alpine];
}

internal class PondTurtle(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Amphibian];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5;
    }
}

internal class PurpleEmperor(TreeIcon treeIcon) : Butterfly(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Butterfly, TypeIcon.Insect];
}

internal class Raccoon(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class RedDeer(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.Deer];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Tree) + forest.NumberOf(TypeIcon.Plant);
    }
}

internal class RedFox(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.Biota.Count(b => b is Hare);
    }
}

internal class RedSquirrel(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return plot.Mainstay is Oak ? 5 : 0;
    }
}

internal class RoeDeer(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Deer, TypeIcon.ClovenHoofedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TreeIcon);
    }
}

internal class SavisPipistrelle(TreeIcon treeIcon) : Bat(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bat, TypeIcon.Alpine];
}

internal class SilverFir() : Tree(TreeIcon.SilverFir)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * plot.Dwellers.Count;
    }
}

internal class SilverWashedFritillary(TreeIcon treeIcon) : Butterfly(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect, TypeIcon.Butterfly];

}

internal class Squeaker(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 1;
    }
}

internal class StagBeetle(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.PawedAnimal);
    }
}

internal class Steinbock(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 10;
    }
}

internal class StingingNettle(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Butterfly);
    }
}

internal class StonePine() : Tree(TreeIcon.StonePine)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree, TypeIcon.Alpine];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Alpine);
    }
}

internal class Sycamore() : Tree(TreeIcon.Sycamore)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Tree);
    }
}

internal class TawnyOwl(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5;
    }
}

internal class TreeFerns(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 6 * forest.NumberOf(TypeIcon.Amphibian);
    }
}

internal class TreeFrog(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Amphibian];
 
    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5 * forest.Biota.Count(b => b is Gnat);
    }
}

internal class VioletCarpenterBee(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class WaterVole(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class WildBoar(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.Biota.Any(b => b is Squeaker) ? 10 : 0;
    }
}

internal class FemaleWildBoar(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.WoodlandEdge];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 10 * forest.Biota.Count(b => b is Squeaker);
    }
}

internal class WildStrawberries(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.DistinctNumberOfTrees() >= 8 ? 10 : 0;
    }
}

internal class Wolf(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5 * forest.NumberOf(TypeIcon.Deer);
    }
}

internal class WoodAnt(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.Plots.Sum(p => p.BottomDwellers.Count);
    }
}

internal class Sapling() : Tree(TreeIcon.None)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override double GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}