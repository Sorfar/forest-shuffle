using biota;
using biota.dwellers;
using biota.habitats;

namespace forest;

public class Plot
{
    public Plot(Habitat mainstay)
    {
        Habitat = mainstay;
    }

    public Habitat Habitat { get; private set; } = default!;

    public List<Dweller> TopDwellers { get; } = [];

    public List<Dweller> BottomDwellers { get; } = [];

    public List<Dweller> LeftDwellers { get; } = [];

    public List<Dweller> RightDwellers { get; } = [];

    public IReadOnlyList<LivingOrganism> Biota => [Habitat, .. TopDwellers, .. BottomDwellers, .. LeftDwellers, .. RightDwellers];

    public IReadOnlyList<LivingOrganism> Dwellers => [.. TopDwellers, .. BottomDwellers, .. LeftDwellers, .. RightDwellers];

    public bool FullyOccupied => 
        TopDwellers.Count != 0 
        && BottomDwellers.Count != 0 
        && LeftDwellers.Count != 0 
        && RightDwellers.Count != 0;

    public void AddDweller(Dweller dweller, DwellerPosition position)
    {
        switch (position)
        {
            case DwellerPosition.Top:
                TopDwellers.Add(dweller);
                break;
            case DwellerPosition.Bottom:
                BottomDwellers.Add(dweller);
                break;
            case DwellerPosition.Left:
                LeftDwellers.Add(dweller);
                break;
            case DwellerPosition.Right:
                RightDwellers.Add(dweller);
                break;
        }
    }
}
