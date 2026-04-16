using biota.habitats.trees;
using forest;

namespace biota.dwellers.common;

public class RedSquirrel(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return plot.Mainstay is Oak ? 5 : 0;
    }
}
