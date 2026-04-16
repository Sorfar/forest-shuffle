using forest;

namespace biota.mainstays.shrubs;

public class Blackthorn(TreeIcon treeIcon) : Shrub(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Shrub, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 0;
    }
}
