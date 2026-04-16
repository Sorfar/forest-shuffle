using forest;

namespace biota.dwellers.common;

public class Fireflies(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Insect];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        var fireFliesCount = forest.Biota.Count(b => b is Fireflies);
        return fireFliesCount switch
        {
            > 0 and <= 4 => fireFliesCount * 5,
            >= 5 => 20,
            _ => 0,
        };
    }
}
