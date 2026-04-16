using forest;

namespace biota.dwellers.woodlandEdge;

public class EuropeanPolecat(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return plot.Dwellers.Count == 1 ? 10 : 0;
    }
}
