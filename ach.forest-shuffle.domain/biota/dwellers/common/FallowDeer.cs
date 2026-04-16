using forest;

namespace biota.dwellers.common;

public class FallowDeer(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Deer, TypeIcon.ClovenHoofedAnimal];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 3 * forest.NumberOf(TypeIcon.ClovenHoofedAnimal);
    }
}
