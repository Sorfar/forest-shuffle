using forest;

namespace biota.dwellers.bats;

public abstract class Bat(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.DistinctNumberOf(TypeIcon.Bat) >= 3 ? 5 : 0;
    }
}
