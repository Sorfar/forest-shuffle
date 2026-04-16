using biota.habitats.trees;
using forest;

namespace biota.habitats;

public class Sapling() : Tree(TreeIcon.None)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 0;
    }
}