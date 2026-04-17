using biota;
using biota.dwellers;
using biota.dwellers.butterflies;
using biota.dwellers.common;
using biota.habitats;
using biota.habitats.trees;

namespace forest;

public class Forest
{
    public Forest()
    {
    }

    private List<Plot> _plots = [];

    public int CaveCardCount { get; private set; }

    public IReadOnlyList<LivingOrganism> Biota => _plots.SelectMany(p => p.Biota).ToList();

    public IReadOnlyList<Plot> Plots => _plots;

    public Plot AddPlot(Habitat mainstay)
    {
        var plot = new Plot(mainstay);

        _plots.Add(plot);

        return plot;
    }

    public void AddDweller(Plot plot, Dweller dweller, DwellerPosition position)
    {
        plot.AddDweller(dweller, position);
    }

    public void SetCaveCardCount(int count)
    {
        CaveCardCount = count;
    }

    public int GetTotalPoints(IReadOnlyList<Forest> otherForests)
    {
        var totalPoints = 0;

        totalPoints += Plots.Sum(p => p.Biota.Sum(b => b.GetPointValue(this, otherForests, p)));

        totalPoints += GetButterflyPoints();

        totalPoints += GetFireSalamanderPoints();

        totalPoints += CaveCardCount;

        return totalPoints;
    }

    internal int NumberOf(TypeIcon typeIcon)
    {
        return Biota.Count(b => b.HasType(typeIcon));
    }

    internal int NumberOf(TreeIcon treeIcon)
    {
        return Biota.Count(b => b.HasType(treeIcon));
    }

    internal int DistinctNumberOf(TypeIcon typeIcon)
    {
        return Biota.Where(b => b.HasType(typeIcon)).Select(b => b.GetType()).Distinct().Count();
    }

    internal int DistinctNumberOfTrees()
    {
        return Biota.Where(b => b.HasType(TypeIcon.Tree) && b.TreeIcon is not TreeIcon.None).Select(b => b.GetType()).Distinct().Count();
    }

    internal int NumberOfTrees()
    {
        return Biota.Count(b => b is Tree) + NumberOfVioletCarpenterBees();
    }

    internal int NumberOfTreesOfType<T>() where T : Tree
    {
        return Biota.OfType<T>().Count() + NumberOfVioletCarpenterBees<T>();
    }

    internal int NumberOfVioletCarpenterBees()
    {
        return Plots.Where(p => p.Mainstay is Tree).Sum(p => p.Biota.Count(b => b is VioletCarpenterBee));
    }

    internal int NumberOfVioletCarpenterBees<T>() where T : Tree
    {
        return Plots.Where(p => p.Mainstay.GetType() == typeof(T)).Sum(p => p.Biota.Count(b => b is VioletCarpenterBee));
    }

    private static List<List<T>> SplitIntoUniqueSets<T>(List<T> items) where T : LivingOrganism
    {
        var result = new List<List<T>>();

        var groups = items
            .GroupBy(b => b!.GetType())
            .ToDictionary(
                g => g.Key,
                g => new Queue<T>(g)
            );

        while (groups.Any(g => g.Value.Count > 0))
        {
            var currentSet = new List<T>();

            foreach (var key in groups.Keys.ToList())
            {
                var queue = groups[key];

                if (queue.Count > 0)
                {
                    currentSet.Add(queue.Dequeue());
                }
            }

            result.Add(currentSet);
        }

        return result;
    }

    private int GetButterflyPoints()
    {
        var butterflies = Biota.OfType<Butterfly>();

        var sets = SplitIntoUniqueSets([.. butterflies]);

        return sets.Sum(GetPointValue);

        static int GetPointValue(List<Butterfly> butterflies)
        {
            return butterflies.Count switch
            {
                2 => 3,
                3 => 6,
                4 => 12,
                5 => 20,
                6 => 35,
                7 => 55,
                >= 8 => 80,
                _ => 0,
            };
        }
    }

    private int GetFireSalamanderPoints()
    {
        var fireSalamanders = Biota.OfType<FireSalamander>();

        return fireSalamanders.Count() switch
        {
            1 => 5,
            2 => 15,
            >= 3 => 25,
            _ => 0
        };
    }

}
