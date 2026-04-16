using forest;

namespace biota.dwellers.common;

public class BeechMarten(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 5 * forest.Plots.Count(p => p.FullyOccupied);
    }
}
