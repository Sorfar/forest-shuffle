using forest;

namespace biota.dwellers.common;

public class TreeFrog(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Amphibian];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 5 * forest.Biota.Count(b => b is Gnat);
    }
}
