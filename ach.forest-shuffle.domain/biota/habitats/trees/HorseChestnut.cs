using forest;

namespace biota.habitats.trees;

public class HorseChestnut() : Tree(TreeIcon.HorseChestnut)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        var count = forest.NumberOfTreesOfType<HorseChestnut>();
        return count >= 7 ? 7 : count;
    }
}
