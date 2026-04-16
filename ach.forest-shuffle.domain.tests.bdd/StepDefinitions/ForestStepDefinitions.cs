using biota;
using biota.dwellers;
using biota.dwellers.butterflies;
using biota.habitats;
using biota.habitats.shrubs;
using biota.habitats.trees;
using forest;
using System.Reflection;
using Xunit;

namespace Ach.Forest_Shuffle.Domain.Tests.Bdd.StepDefinitions;

[Binding]
public class ForestStepDefinitions
{
    private ScenarioContext _scenarioContext;

    public ForestStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _scenarioContext["currentForest"] = new Forest();
        _scenarioContext["otherForest"] = new Forest();
    }

    [Given(@"a plot with (a )(an ){} tree as habitat")]
    public void GivenAPlotWithTreeAsHabitat(Type treeType)
    {
        var tree = CreateInstanceOfTree(treeType);
        var plot = GetCurrentForest().AddPlot(tree);
        SetCurrentPlot(plot);
    }

    [Given(@"a plot with a sapling as habitat")]
    public void GivenAPlotWithSaplingAsHabitat()
    {
        var tree = CreateInstanceOfTree(typeof(Sapling));
        var plot = GetCurrentForest().AddPlot(tree);
        SetCurrentPlot(plot);
    }

    [Given(@"a plot with (a )(an ){} shrub as habitat")]
    public void GivenAPlotWithShrubAsHabitat(Type shrubType)
    {
        var shrub = CreateInstanceOfShrub(shrubType);
        var plot = GetCurrentForest().AddPlot(shrub);
        SetCurrentPlot(plot);
    }

    [Given(@"(a )(an ){} on the {DwellerPosition}")]
    public void GivenADwellerOnPosition(Type dwellerType, DwellerPosition position)
    {
        var plot = GetCurrentPlot() ?? throw new InvalidOperationException("No plot.");
        var dweller = CreateInstanceOfDweller(dwellerType, TreeIcon.None);
        GetCurrentForest().AddDweller(plot, dweller, position);
    }

    [Given(@"(a )(an ){} of tree type {TreeIcon} on the {DwellerPosition}")]
    public void GivenADwellerOnPosition(Type dwellerType, TreeIcon treeIcon, DwellerPosition position)
    {
        var plot = GetCurrentPlot() ?? throw new InvalidOperationException("No plot.");
        var dweller = CreateInstanceOfDweller(dwellerType, treeIcon);
        GetCurrentForest().AddDweller(plot, dweller, position);
    }

    [When(@"I get the number of living organism of type icon '{TypeIcon}'")]
    public void WhenGetNumberOfTypeIcon(TypeIcon typeIcon)
    {
        SetNumberResult(GetCurrentForest().NumberOf(typeIcon));
    }

    [When(@"I get the number of living organism of tree type '{TreeIcon}'")]
    public void WhenGetNumberOfTreeIcon(TreeIcon treeIcon)
    {
        SetNumberResult(GetCurrentForest().NumberOf(treeIcon));
    }

    [When(@"I get the distinct number of living organism of type icon '{TypeIcon}'")]
    public void WhenGetDistinctNumberOfTypeIcon(TypeIcon typeIcon)
    {
        SetNumberResult(GetCurrentForest().DistinctNumberOf(typeIcon));
    }

    [When(@"I get the distinct number of trees")]
    public void WhenGetDistinctNumberOfTrees()
    {
        SetNumberResult(GetCurrentForest().DistinctNumberOfTrees());
    }

    [When(@"I get the number of trees")]
    public void WhenGetNumberOfTrees()
    {
        SetNumberResult(GetCurrentForest().NumberOfTrees());
    }

    [When(@"I get the number of trees of type {}")]
    public void WhenGetNumberOfTrees(Type type)
    {
        var method = typeof(Forest)
            .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
            .FirstOrDefault(m => m.Name == "NumberOfTreesOfType" && m.IsGenericMethod)
            ?? throw new InvalidOperationException();

        var constructed = method.MakeGenericMethod(type);

        var result = constructed.Invoke(GetCurrentForest(), null) as int? ?? throw new InvalidOperationException();

        SetNumberResult(result);
    }

    [Then(@"the number result should be {int}")]
    public void ThenTheNumberResultShouldBe(int number)
    {
        Assert.Equal(number, GetNumberResult());
    }

    private Tree CreateInstanceOfTree(Type treeType)
    {
        var tree = Activator.CreateInstance(treeType) as Tree;
        return tree ?? throw new InvalidCastException($"Couldn't cast {treeType.Name} as a tree.");
    }

    private Shrub CreateInstanceOfShrub(Type shrubType)
    {
        var shrub = Activator.CreateInstance(shrubType) as Shrub;
        return shrub ?? throw new InvalidCastException($"Couldn't cast {shrubType.Name} as a shrub.");
    }

    private Dweller CreateInstanceOfDweller(Type dwellerType, TreeIcon treeIcon)
    {
        var dweller = Activator.CreateInstance(dwellerType, treeIcon) as Dweller;
        return dweller ?? throw new InvalidCastException($"Couldn't cast{dwellerType.Name} as a dweller.");
    }

    private Forest GetCurrentForest()
    {
        return (_scenarioContext["currentForest"] as Forest)!;
    }

    private Forest GetOtherForest()
    {
        return (_scenarioContext["otherForest"] as Forest)!;
    }

    private Plot? GetCurrentPlot()
    {
        return _scenarioContext["currentPlot"] as Plot;
    }

    private void SetCurrentPlot(Plot plot)
    {
        _scenarioContext["currentPlot"] = plot;
    }

    private void SetNumberResult(int count)
    {
        _scenarioContext["count"] = count;
    }

    private int GetNumberResult() 
    {
        return (int)_scenarioContext["count"];
    }
}