using forest;

namespace biota.dwellers.woodlandEdge;

public class StingingNettle(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Butterfly);
    }
}
