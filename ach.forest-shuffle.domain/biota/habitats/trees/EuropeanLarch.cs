using forest;

namespace biota.habitats.trees;

public class EuropeanLarch() : Tree(TreeIcon.EuropeanLarch)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree, TypeIcon.Alpine];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 3;
    }
}
