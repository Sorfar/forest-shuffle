using forest;

namespace biota.dwellers.woodlandEdge;

public class BarnOwl(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TypeIcon.Bat);
    }
}
