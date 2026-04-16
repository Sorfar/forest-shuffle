using forest;

namespace biota.dwellers.common;

public class Wolf(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 5 * forest.NumberOf(TypeIcon.Deer);
    }
}
