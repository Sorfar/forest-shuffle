using forest;

namespace biota.dwellers.common;

public class WoodAnt(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 2 * forest.Plots.Sum(p => p.BottomDwellers.Count);
    }
}
