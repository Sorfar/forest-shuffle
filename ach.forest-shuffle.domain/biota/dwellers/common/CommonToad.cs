using forest;

namespace biota.dwellers.common;

public class CommonToad(TreeIcon treeIcon) : Dweller(treeIcon)
{
    public override List<TypeIcon> TypeIcons => [TypeIcon.Amphibian];

    public override int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot)
    {
        return plot.BottomDwellers.Count(d => d.GetType() == typeof(CommonToad)) == 2 ? 5 : 0;
    }
}
