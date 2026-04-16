using forest;

namespace biota.dwellers.common;

public class Lynx(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.Biota.Any(b => b is RoeDeer) ? 10 : 0;
    }
}
