using forest;

namespace biota.dwellers.woodlandEdge;

public class EuropeanBison(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 2 * forest.NumberOf(TreeIcon.Beech) + 2 * forest.NumberOf(TreeIcon.Oak);
    }
}
