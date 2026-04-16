using forest;

namespace biota.dwellers.common;

public class TreeFerns(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Plant];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 6 * forest.NumberOf(TypeIcon.Amphibian);
    }
}
