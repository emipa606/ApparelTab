using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace ApparelTab.GUI;

public sealed class Filter : IExposable
{
    public ApparelLayerDef apparelLayerDef;

    public string filterString = "";

    public List<HashApparel> filteredApparels = Utilities.HashApparels;

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

        var sourceList = filteredApparels.ToList();
        sourceList.Sort((a, b) =>
        {
            int r;

            if (sortByLevel.HasValue)
            {
                r = ((int)a.techLevel).CompareTo((int)b.techLevel);
                if (!sortByLevel.Value)
                {
                    r = -r;
                }

                if (r != 0)
                {
                    return r;
                }
            }

            if (sortByLayer.HasValue)
            {
                var aLayer = a.apparelLayerDef?.ToString() ?? "";
                var bLayer = b.apparelLayerDef?.ToString() ?? "";
                r = string.Compare(aLayer, bLayer, StringComparison.OrdinalIgnoreCase);
                if (!sortByLayer.Value)
                {
                    r = -r;
                }

                if (r != 0)
                {
                    return r;
                }
            }

            if (sortByArmorBlunt.HasValue)
            {
                r = a.armorBlunt.CompareTo(b.armorBlunt);
                if (!sortByArmorBlunt.Value)
                {
                    r = -r;
                }

                if (r != 0)
                {
                    return r;
                }
            }

            if (sortByArmorSharp.HasValue)
            {
                r = a.armorSharp.CompareTo(b.armorSharp);
                if (!sortByArmorSharp.Value)
                {
                    r = -r;
                }

                if (r != 0)
                {
                    return r;
                }
            }

            if (sortByArmorHeat.HasValue)
            {
                r = a.armorHeat.CompareTo(b.armorHeat);
                if (!sortByArmorHeat.Value)
                {
                    r = -r;
                }

                if (r != 0)
                {
                    return r;
                }
            }

            if (sortByInsulationCold.HasValue)
            {
                r = a.insulation_Cold.CompareTo(b.insulation_Cold);
                if (!sortByInsulationCold.Value)
                {
                    r = -r;
                }

                if (r != 0)
                {
                    return r;
                }
            }

            if (sortByInsulationHeat.HasValue)
            {
                r = a.insulation_Heat.CompareTo(b.insulation_Heat);
                if (!sortByInsulationHeat.Value)
                {
                    r = -r;
                }

                if (r != 0)
                {
                    return r;
                }
            }

            if (sortByMod.HasValue)
            {
                var aMod = a.mod ?? "";
                var bMod = b.mod ?? "";
                r = string.Compare(aMod, bMod, StringComparison.OrdinalIgnoreCase);
                if (!sortByMod.Value)
                {
                    r = -r;
                }

                if (r != 0)
                {
                    return r;
                }
            }

            var aLabel = a.def == null ? "" : a.def.LabelCap.ToString();
            var bLabel = b.def == null ? "" : b.def.LabelCap.ToString();
            return string.Compare(aLabel, bLabel, StringComparison.OrdinalIgnoreCase);
        });

        filteredApparels = sourceList;
        return filteredApparels;
    }
}