using forest;

namespace biota.dwellers.hares;

public abstract class Hare(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.Biota.Count(b => b is Hare);
    }
}
