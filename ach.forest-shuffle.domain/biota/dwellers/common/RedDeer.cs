using forest;

namespace biota.dwellers.common;

public class RedDeer(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.Deer];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.Tree) + forest.NumberOf(TypeIcon.Plant);
    }
}
