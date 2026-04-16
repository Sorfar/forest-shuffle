using forest;

namespace biota.habitats.trees;

public class Oak() : Tree(TreeIcon.Oak)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.DistinctNumberOfTrees() >= 8 ? 10 : 0;
    }
}
