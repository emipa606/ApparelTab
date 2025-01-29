using System;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace ApparelTab.GUI;

public class HashApparel : IExposable
{
    public string apparelLayerDef;

    public float armorBlunt;

    public float armorHeat;

    public float armorSharp;
    public ThingDef def;

    public float insulation_Cold;

    public float insulation_Heat;

    public string mod;

    public TechLevel techLevel;

    public Texture2D texture2D;

    public void ExposeData()
    {
    }

    public void PreAddSettings(ThingDef thingDef)
    {
        def = thingDef;
        if (def.graphicData is { texPath: not null })
        {
            texture2D = ContentFinder<Texture2D>.Get(def.graphicData.texPath, false);
            if (texture2D == null)
            {
                texture2D = ContentFinder<Texture2D>.GetAllInFolder(def.graphicData.texPath)?.Last();
            }
        }

        apparelLayerDef = def.apparel.LastLayer != null
            ? def.apparel.LastLayer.LabelCap.ToString()
            : "ApparelTab.GUI.Unknown".Translate().ToString();
        techLevel = thingDef.techLevel;
        armorBlunt = GenStuff.DefaultStuffFor(def) == null
            ? def.statBases.GetStatValueFromList(StatDefOf.ArmorRating_Blunt, 0f)
            : GenStuff.DefaultStuffFor(def).statBases.GetStatValueFromList(StatDefOf.StuffPower_Armor_Blunt, 0f) *
              def.statBases.GetStatValueFromList(StatDefOf.StuffEffectMultiplierArmor, 0f);
        armorSharp = GenStuff.DefaultStuffFor(def) == null
            ? def.statBases.GetStatValueFromList(StatDefOf.ArmorRating_Sharp, 0f)
            : GenStuff.DefaultStuffFor(def).statBases.GetStatValueFromList(StatDefOf.StuffPower_Armor_Sharp, 0f) *
              def.statBases.GetStatValueFromList(StatDefOf.StuffEffectMultiplierArmor, 0f);
        armorHeat = GenStuff.DefaultStuffFor(def) == null
            ? def.statBases.GetStatValueFromList(StatDefOf.ArmorRating_Heat, 0f)
            : GenStuff.DefaultStuffFor(def).statBases.GetStatValueFromList(StatDefOf.StuffPower_Armor_Heat, 0f) *
              def.statBases.GetStatValueFromList(StatDefOf.StuffEffectMultiplierArmor, 0f);
        insulation_Cold = GenStuff.DefaultStuffFor(def) != null
            ? GenStuff.DefaultStuffFor(def).statBases.GetStatValueFromList(StatDefOf.StuffPower_Insulation_Cold, 0f) *
              def.statBases.GetStatValueFromList(StatDefOf.StuffEffectMultiplierInsulation_Cold, 1f)
            : def.statBases.GetStatValueFromList(StatDefOf.Insulation_Cold, 0f);
        insulation_Heat = GenStuff.DefaultStuffFor(def) != null
            ? GenStuff.DefaultStuffFor(def).statBases.GetStatValueFromList(StatDefOf.StuffPower_Insulation_Heat, 0f) *
              def.statBases.GetStatValueFromList(StatDefOf.StuffEffectMultiplierInsulation_Heat, 1f)
            : def.statBases.GetStatValueFromList(StatDefOf.Insulation_Heat, 0f);
        armorBlunt = (float)Math.Round(armorBlunt, 4);
        armorSharp = (float)Math.Round(armorSharp, 4);
        armorHeat = (float)Math.Round(armorHeat, 4);
        insulation_Cold = (float)Math.Round(insulation_Cold, 4);
        insulation_Heat = (float)Math.Round(insulation_Heat, 4);
        mod = def.modContentPack is { Name: not null }
            ? def.modContentPack.Name
            : "ApparelTab.GUI.Unknown".Translate().ToString();
    }
}