using forest;

namespace biota.mainstays.trees;

public class Sycamore() : Tree(TreeIcon.Sycamore)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Tree);
    }
}
