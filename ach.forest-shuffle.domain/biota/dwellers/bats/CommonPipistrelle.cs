namespace biota.dwellers.bats;

public class CommonPipistrelle(TreeIcon treeIcon) : Bat(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Bat, TypeIcon.WoodlandEdge];
}
