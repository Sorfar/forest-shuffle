using forest;

namespace biota;

public abstract class LivingOrganism(TreeIcon treeIcon)
{
    public TreeIcon TreeIcon { get; } = treeIcon;

    public abstract List<TypeIcon> TypeIcons { get; }

    public abstract int GetPointValue(Forest forest, IReadOnlyList<Forest> otherForests, Plot plot);

    public bool HasType(TypeIcon typeIcon)
    {
        return TypeIcons.Contains(typeIcon);
    }

    public bool HasType(TreeIcon treeIcon)
    {
        return TreeIcon == treeIcon;
    }
}
