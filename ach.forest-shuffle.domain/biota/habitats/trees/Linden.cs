using forest;

namespace biota.habitats.trees;

public class Linden() : Tree(TreeIcon.Linden)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Tree];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return otherForests.All(of => of.NumberOfTreesOfType<Linden>() < forest.NumberOfTreesOfType<Linden>()) ? 3 : 1;
    }
}
