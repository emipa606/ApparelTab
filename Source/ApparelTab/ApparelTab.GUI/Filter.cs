using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace ApparelTab.GUI;

public sealed class Filter : IExposable
{
    public ApparelLayerDef apparelLayerDef;

    public List<HashApparel> filteredApparels = Utilities.HashApparels;

    public string filterString = "";

    public ModContentPack mod;

    public bool? sortByArmorBlunt;

    public bool? sortByArmorHeat;

    public bool? sortByArmorSharp;

    public bool? sortByInsulationCold;

    public bool? sortByInsulationHeat;

    public bool? sortByLayer;

    public bool? sortByLevel;

    public bool? sortByMod;
    public TechLevel techLevel;

    public void ExposeData()
    {
        Scribe_Deep.Look(ref filteredApparels, "filteredApparels", LookMode.Def);
    }

    public List<HashApparel> FilteredApparels()
    {
        var list = new List<HashApparel>();
        list.AddRange(Utilities.HashApparels);
        filteredApparels = list.Where(x =>
            (techLevel == TechLevel.Undefined || x.techLevel == techLevel) &&
            (apparelLayerDef == null || x.apparelLayerDef == apparelLayerDef.LabelCap) &&
            (mod == null || x.mod == mod.Name) &&
            (filterString == "" || x.def.LabelCap.ToString().ToLower().Contains(filterString.ToLower()))).ToList();
        var source = from x in filteredApparels
            orderby 0, sortByLevel.HasValue ? sortByLevel != true ? 0 - x.techLevel : (int)x.techLevel : 0,
                sortByLayer.HasValue
                    ? sortByLayer != true ? x.apparelLayerDef.Reverse().ToString() : x.apparelLayerDef
                    : "", sortByArmorBlunt.HasValue ? sortByArmorBlunt != true ? 0f - x.armorBlunt : x.armorBlunt : 0f,
                sortByArmorSharp.HasValue ? sortByArmorSharp != true ? 0f - x.armorSharp : x.armorSharp : 0f,
                sortByArmorHeat.HasValue ? sortByArmorHeat != true ? 0f - x.armorHeat : x.armorHeat : 0f,
                sortByInsulationCold.HasValue
                    ? sortByInsulationCold != true ? 0f - x.insulation_Cold : x.insulation_Cold
                    : 0f, sortByInsulationHeat.HasValue
                    ? sortByInsulationHeat != true ? 0f - x.insulation_Heat : x.insulation_Heat
                    : 0f, sortByMod != true ? "" : x.mod, sortByMod != false ? "" : x.mod descending, x.def.LabelCap
                    .ToString()
            select x;
        filteredApparels = source.ToList();
        return filteredApparels;
    }
}