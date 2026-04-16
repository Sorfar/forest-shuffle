using biota.habitats.trees;

namespace biota;

public enum TypeIcon
{
    Amphibian,
    Tree,
    Bat,
    Deer,
    Insect,
    ClovenHoofedAnimal,
    Plant,
    PawedAnimal,
    Mushroom,
    Butterfly,
    Bird,
    Alpine,
    Shrub,
    WoodlandEdge,

}

public static class TypeIconExtensions
{
    public static bool HasType(this Tree biota, TypeIcon typeIcon)
    {
        return biota.TypeIcons.Contains(typeIcon);
    }
}