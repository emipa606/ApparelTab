using System.Collections.Generic;
using System.Linq;
using ApparelTab.GUI;
using UnityEngine;
using Verse;

namespace ApparelTab;

[StaticConstructorOnStartup]
public static class Utilities
{
    public static readonly List<HashApparel> hashApparels;

    public static readonly List<ApparelLayerDef> apparelLayerDefs;

    public static readonly List<ModContentPack> mods;

    static Utilities()
    {
        hashApparels = [];
        apparelLayerDefs = [];
        mods = [];
        foreach (var item in DefDatabase<ThingDef>.AllDefsListForReading.Where(d => d.IsApparel).ToList())
        {
            var hashApparel = new HashApparel();
            hashApparel.PreAddSettings(item);
            hashApparels.Add(hashApparel);
            if (item.modContentPack != null && !mods.Contains(item.modContentPack))
            {
                mods.Add(item.modContentPack);
            }

            if (item.apparel.LastLayer != null && !apparelLayerDefs.Contains(item.apparel.LastLayer))
            {
                apparelLayerDefs.Add(item.apparel.LastLayer);
            }
        }
    }

    public static Color BgColorButton => new Color(21f / 128f, 21f / 128f, 21f / 128f);

    public static Color BgColorDarkButton => new Color(0.5f, 0.5f, 0.5f);
}