using biota.dwellers.common;
using forest;

namespace biota.dwellers.woodlandEdge;

public class FemaleWildBoar(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.ClovenHoofedAnimal, TypeIcon.WoodlandEdge];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 10 * forest.Biota.Count(b => b is Squeaker);
    }
}
