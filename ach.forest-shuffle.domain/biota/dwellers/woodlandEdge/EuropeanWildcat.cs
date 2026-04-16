using forest;

namespace biota.dwellers.woodlandEdge;

public class EuropeanWildcat(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.WoodlandEdge);
    }
}
