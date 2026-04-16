using forest;

namespace biota.mainstays.trees;

public class HorseChestnut() : Tree(TreeIcon.HorseChestnut)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        var count = forest.NumberOfTree(this);
        return count >= 7 ? 7 : count;
    }
}
