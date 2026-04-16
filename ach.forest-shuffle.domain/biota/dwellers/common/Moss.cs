using forest;

namespace biota.dwellers.common;

public class Moss(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.NumberOfTrees() >= 10 ? 10 : 0;
    }
}
