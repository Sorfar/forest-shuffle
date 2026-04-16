using forest;

namespace biota.dwellers.alpine;

public class AlpineMarmot(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.Alpine];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return 3 * forest.DistinctNumberOf(TypeIcon.Plant);
    }
}
