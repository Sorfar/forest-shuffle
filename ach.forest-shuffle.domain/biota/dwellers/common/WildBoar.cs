using forest;

namespace biota.dwellers.common;

public class WildBoar(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.Biota.Any(b => b is Squeaker) ? 10 : 0;
    }
}
