using forest;

namespace biota.dwellers.common;

public class Bullfinch(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Insect);
    }
}
