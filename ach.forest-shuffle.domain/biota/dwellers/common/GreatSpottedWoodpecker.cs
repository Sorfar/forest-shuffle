using forest;

namespace biota.dwellers.common;

public class GreatSpottedWoodpecker(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return otherForests.All(of => of.NumberOfTree() < forest.NumberOfTree()) ? 10 : 0;
    }
}
