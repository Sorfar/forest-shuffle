using biota.dwellers.butterflies;
using forest;

namespace biota.dwellers.woodlandEdge;

public class MapButterfly(TreeIcon treeIcon) : Butterfly(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Butterfly, TypeIcon.Insect, TypeIcon.WoodlandEdge];
}
