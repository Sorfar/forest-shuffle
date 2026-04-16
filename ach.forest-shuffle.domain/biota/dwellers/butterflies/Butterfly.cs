using forest;

namespace biota.dwellers.butterflies;

public abstract class Butterfly(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 0;
    }
}
