using forest;

namespace biota.dwellers.alpine;

public class Steinbock(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.Alpine];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 10;
    }
}
