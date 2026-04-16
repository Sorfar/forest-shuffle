using forest;

namespace biota.dwellers.woodlandEdge;

public class Digitalis(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.DistinctNumberOf(TypeIcon.Plant) switch
        {
            1 => 1,
            2 => 3,
            3 => 6,
            4 => 10,
            >= 5 => 15,
            _ => 0,
        };
    }
}
