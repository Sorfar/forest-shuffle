using forest;

namespace biota.dwellers.woodlandEdge;

public class BeeSwarm(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Plant);
    }
}
