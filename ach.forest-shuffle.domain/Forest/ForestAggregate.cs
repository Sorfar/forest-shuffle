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

    public int DistinctNumberOf(TypeIcon typeIcon)
    {
        return Biota.Where(b => b.HasType(typeIcon)).Select(b => b.GetType()).Distinct().Count();
    }

    public int NumberOfSameTree(Tree tree)
    {
        return Biota.Count(b => b.GetType() == tree.GetType());
    }
}

internal class Plot
{
    private Plot()
    {

    }

    public Mainstay Mainstay { get; private set; }

    public Dweller? TopDweller { get; private set; }

    public Dweller? BottomDweller { get; private set; }

    public Dweller? LeftDweller { get; private set; }

    public Dweller? RightDweller { get; private set; }

    public IReadOnlyList<Biota?> Biota => new List<Biota?> { Mainstay, TopDweller, BottomDweller, LeftDweller, RightDweller };

    public bool FullyOccupied => Biota.All(b => b != null);

    public static Plot Create(Mainstay mainstay, Dweller? topDweller, Dweller? bottomDweller, Dweller? leftDweller, Dweller? rightDweller)
    {
        return new Plot()
        {
            Mainstay = mainstay,
            TopDweller = topDweller,
            BottomDweller = bottomDweller,
            LeftDweller = leftDweller,
            RightDweller = rightDweller
        };
    }

}

internal abstract class Mainstay : Biota
{
}

internal abstract class Tree : Mainstay
{
}

internal abstract class Shrub : Mainstay
{
}

internal abstract class Dweller : Biota
{
}

internal abstract class Butterfly : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal abstract class Biota
{
    private List<TypeIcon> _typeIcons = [];

    public IReadOnlyList<TypeIcon> TypeIcons => _typeIcons;

    public TreeIcon TreeIcon { get; private set; }

    public abstract int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot);

    public bool HasType(TypeIcon typeIcon)
    {
        return TypeIcons.Contains(typeIcon);
    }
}

internal enum TreeIcon
{
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

internal class AlpineMarmot : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.DistinctNumberOf(TypeIcon.Plant);
    }
}

internal class AlpineNewt : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Insect);
    }
}

internal class BarbastelleBat : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.DistinctNumberOf(TypeIcon.Bat) >= 3 ? 5 : 0;
    }
}

internal class BarnOwl : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TypeIcon.Bat);
    }
}

internal class BeardedVulture : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.Cave;
    }
}

internal class BeeSwarm : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Plant);
    }
}

internal class BechsteinsBat : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.DistinctNumberOf(TypeIcon.Bat) >= 3 ? 5 : 0;
    }
}

internal class Beech : Tree
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        var numberOfBeeches = forest.NumberOfSameTree(this);

        var numberOfVioletCarpenterBees = forest.Plots.Where(p => p.Mainstay is Beech && p.Biota.Any(b => b is CarpenterBee));

        return forest.NumberOfSameTree(this) >= 4 ? 5 : 0;
    }
}

internal class BeechMarten : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5 * forest.Plots.Count(p => p.FullyOccupied);
    }
}

internal class Birch : Tree
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 1;
    }
}

internal class BlackTrumpet : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class Blackthorn : Shrub
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}

internal class Blackberries : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Plant);
    }
}

internal class BlueBerry : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 2 * forest.DistinctNumberOf(TypeIcon.Bird);
    }
}

internal class Brimstone : Butterfly
{
}

internal class BrownBear : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {

    }
}

internal class TawnyOwl : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 5;
    }
}

internal class StagBeetle : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return forest.Count(f => f.);
    }
}

internal class CarpenterBee : Dweller
{
    public override int GetPointValue(ForestAggregate forest, IReadOnlyList<ForestAggregate> otherForests, Plot plot)
    {
        return 0;
    }
}