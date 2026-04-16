using forest;

namespace biota.mainstays.trees;

public class Beech() : Tree(TreeIcon.Beech)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        var numberOfBeeches = forest.NumberOfTree(this);

        return forest.NumberOfTree(this) >= 4 ? 5 : 0;
    }
}
