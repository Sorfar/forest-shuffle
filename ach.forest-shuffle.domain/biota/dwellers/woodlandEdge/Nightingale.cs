using biota.habitats.shrubs;
using forest;

namespace biota.dwellers.woodlandEdge;

public class Nightingale(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return plot.Mainstay is Shrub ? 5 : 0;
    }
}
