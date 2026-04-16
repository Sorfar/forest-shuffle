using forest;

namespace biota.dwellers.common;

public class WildStrawberries(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.DistinctNumberOfTrees() >= 8 ? 10 : 0;
    }
}
