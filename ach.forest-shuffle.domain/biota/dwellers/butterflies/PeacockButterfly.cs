namespace biota.dwellers.butterflies;

public class PeacockButterfly(TreeIcon treeIcon) : Butterfly(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Butterfly, TypeIcon.Insect];
}
