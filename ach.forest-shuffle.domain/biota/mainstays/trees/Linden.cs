using forest;

namespace biota.mainstays.trees;

public class Linden() : Tree(TreeIcon.Linden)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return otherForests.All(of => of.NumberOfTree(this) < forest.NumberOfTree(this)) ? 3 : 1;
    }
}
