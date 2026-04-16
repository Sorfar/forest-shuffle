using forest;

namespace biota.habitats.trees;

public class Beech() : Tree(TreeIcon.Beech)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.NumberOfTreesOfType<Beech>() >= 4 ? 5 : 0;
    }
}
