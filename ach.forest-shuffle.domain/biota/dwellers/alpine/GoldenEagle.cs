using forest;

namespace biota.dwellers.alpine;

public class GoldenEagle(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bird, TypeIcon.Alpine];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return forest.NumberOf(TypeIcon.PawedAnimal) + forest.NumberOf(TypeIcon.Amphibian);
    }
}
