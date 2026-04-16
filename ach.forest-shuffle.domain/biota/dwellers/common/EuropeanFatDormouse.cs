using forest;

namespace biota.dwellers.common;

public class EuropeanFatDormouse(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return plot.Biota.Any(b => b.HasType(TypeIcon.Bat)) ? 15 : 0;
    }
}
