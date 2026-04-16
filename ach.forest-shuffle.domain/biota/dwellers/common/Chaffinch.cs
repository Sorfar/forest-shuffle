using biota.habitats.trees;
using forest;

namespace biota.dwellers.common;

public class Chaffinch(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return plot.Mainstay is Beech ? 5 : 0;
    }
}
