namespace biota.dwellers.hares;

public class MountainHare(TreeIcon treeIcon) : Hare(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.PawedAnimal, TypeIcon.Alpine];
}
