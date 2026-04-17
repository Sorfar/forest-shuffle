using biota;
using biota.dwellers;
using biota.habitats;

namespace forest;

public class Plot
{
    public Plot(Habitat mainstay)
    {
        Mainstay = mainstay;
    }

    public Habitat Mainstay { get; private set; } = default!;

    public List<Dweller> TopDwellers { get; } = [];

    public List<Dweller> BottomDwellers { get; } = [];

    public List<Dweller> LeftDwellers { get; } = [];

    public List<Dweller> RightDwellers { get; } = [];

    public IReadOnlyList<LivingOrganism> Biota => [Mainstay, .. TopDwellers, .. BottomDwellers, .. LeftDwellers, .. RightDwellers];

    public IReadOnlyList<LivingOrganism> Dwellers => [.. TopDwellers, .. BottomDwellers, .. LeftDwellers, .. RightDwellers];

    public bool FullyOccupied => Biota.All(b => b != null);

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
