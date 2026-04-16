using forest;

namespace biota.habitats.trees;

public class StonePine() : Tree(TreeIcon.StonePine)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree, TypeIcon.Alpine];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Alpine);
    }
}
