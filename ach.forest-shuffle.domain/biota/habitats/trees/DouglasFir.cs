using forest;

namespace biota.habitats.trees;

public class DouglasFir() : Tree(TreeIcon.DouglasFir)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 5;
    }
}
