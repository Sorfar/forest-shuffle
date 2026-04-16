using forest;

namespace biota.mainstays.trees;

public class SilverFir() : Tree(TreeIcon.SilverFir)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 2 * plot.Dwellers.Count;
    }
}
