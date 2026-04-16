using forest;

namespace biota.dwellers.alpine;

public class CommonRaven(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.Alpine];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 5;
    }
}
