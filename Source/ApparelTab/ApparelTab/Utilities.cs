using System.Collections.Generic;
using System.Linq;
using ApparelTab.GUI;
using UnityEngine;
using Verse;

namespace ApparelTab;

[StaticConstructorOnStartup]
public static class Utilities
{
    public static readonly List<HashApparel> HashApparels;

    public static readonly List<ApparelLayerDef> ApparelLayerDefs;

    public static readonly List<ModContentPack> Mods;

    static Utilities()
    {
        HashApparels = [];
        ApparelLayerDefs = [];
        Mods = [];
        foreach (var item in DefDatabase<ThingDef>.AllDefsListForReading.Where(d => d.IsApparel).ToList())
        {
            var hashApparel = new HashApparel();
            hashApparel.PreAddSettings(item);
            HashApparels.Add(hashApparel);
            if (item.modContentPack != null && !Mods.Contains(item.modContentPack))
            {
                Mods.Add(item.modContentPack);
            }

            if (item.apparel.LastLayer != null && !ApparelLayerDefs.Contains(item.apparel.LastLayer))
            {
                ApparelLayerDefs.Add(item.apparel.LastLayer);
            }
        }
    }

    public static Color BgColorButton => new(21f / 128f, 21f / 128f, 21f / 128f);

    public static Color BgColorDarkButton => new(0.5f, 0.5f, 0.5f);
}