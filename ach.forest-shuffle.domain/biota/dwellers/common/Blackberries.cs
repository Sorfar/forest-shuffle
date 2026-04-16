using forest;

namespace biota.dwellers.common;

public class Blackberries(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TypeIcon.Plant);
    }
}
