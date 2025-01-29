using System;
using System.Collections.Generic;
using ApparelTab.GameComponents;
using RimWorld;
using UnityEngine;
using Verse;

namespace ApparelTab.GUI;

public class MainTabWindow_ApparelTab : MainTabWindow
{
    public Vector2 scrollPosition = Vector2.zero;

    public MainTabWindow_ApparelTab()
    {
        resizeable = false;
        draggable = false;
        forcePause = true;
        doCloseX = false;
    }

    public override Vector2 InitialSize => new Vector2(1360f, 700f);

    public Filter Filter => Current.Game.GetComponent<GameComponent_ApparelTab>().filter;

    protected override void SetInitialSizeAndPosition()
    {
        windowRect = new Rect((UI.screenWidth - InitialSize.x) / 2f, (UI.screenHeight - InitialSize.y) / 2f,
            InitialSize.x, InitialSize.y);
        windowRect = windowRect.Rounded();
    }

    public override void DoWindowContents(Rect inRect)
    {
        Text.Font = GameFont.Small;
        var rect = new Rect(0f, 0f, 80f, 60f);
        Widgets.CustomButtonText(ref rect, "ApparelTab.GUI.Icon".Translate(), Utilities.BgColorButton, Color.white,
            Color.white);
        var rect2 = new Rect(rect.x + rect.width + 10f, rect.y, 300f, rect.height);
        Widgets.CustomButtonText(ref rect2, "ApparelTab.GUI.Label".Translate(), Utilities.BgColorButton, Color.white,
            Color.white);
        Widgets.Label(new Rect(rect2.x + 55f, rect2.y + rect2.height + 10f, 80f, 30f),
            "ApparelTab.GUI.Search".Translate());
        var rect3 = new Rect(rect.x, rect2.y + rect2.height + 5f, rect.x + rect.width + rect2.width + 10f, 30f);
        Filter.filterString = Widgets.TextEntryLabeled(rect3, "", Filter.filterString);
        var rect4 = new Rect(rect2.x + rect2.width + 10f, rect2.y, 100f, rect2.height);
        if (Widgets.CustomButtonText(ref rect4,
                "ApparelTab.GUI.TechLevel".Translate() + ": \n" + Filter.techLevel.ToString(), Utilities.BgColorButton,
                Color.white, Color.white))
        {
            var list = new List<FloatMenuOption>();
            var array = (TechLevel[])Enum.GetValues(typeof(TechLevel));
            foreach (var techLevel in array)
            {
                var level = techLevel;
                list.Add(new FloatMenuOption(techLevel.ToString(), delegate
                {
                    Filter.techLevel = level;
                    Filter.FilteredApparels();
                }));
            }

            Find.WindowStack.Add(new FloatMenu(list));
        }

        Button(rect4, ref Filter.sortByLevel);
        var rect5 = new Rect(rect4.x + rect4.width + 10f, rect4.y, 120f, rect4.height);
        string text = Filter.apparelLayerDef?.LabelCap ?? "ApparelTab.GUI.Null".Translate();
        if (Widgets.CustomButtonText(ref rect5, "ApparelTab.GUI.ApparelLayerDef".Translate() + ": \n" + text,
                Utilities.BgColorButton, Color.white, Color.white))
        {
            var list2 = new List<FloatMenuOption>();
            foreach (var apparelLayerDef in Utilities.apparelLayerDefs)
            {
                list2.Add(new FloatMenuOption(apparelLayerDef.LabelCap, delegate
                {
                    Filter.apparelLayerDef = apparelLayerDef;
                    Filter.FilteredApparels();
                }));
            }

            list2.Add(new FloatMenuOption("ApparelTab.GUI.Null".Translate(), delegate
            {
                Filter.apparelLayerDef = null;
                Filter.FilteredApparels();
            }));
            Find.WindowStack.Add(new FloatMenu(list2));
        }

        Button(rect5, ref Filter.sortByLayer);
        var rect6 = new Rect(rect5.x + rect5.width + 10f, rect5.y, 100f, rect5.height);
        Widgets.CustomButtonText(ref rect6, "ApparelTab.GUI.ArmorBlunt".Translate(), Utilities.BgColorButton,
            Color.white, Color.white);
        Button(rect6, ref Filter.sortByArmorBlunt);
        var rect7 = new Rect(rect6.x + rect6.width + 10f, rect6.y, 100f, rect6.height);
        Widgets.CustomButtonText(ref rect7, "ApparelTab.GUI.ArmorSharp".Translate(), Utilities.BgColorButton,
            Color.white, Color.white);
        Button(rect7, ref Filter.sortByArmorSharp);
        var rect8 = new Rect(rect7.x + rect7.width + 10f, rect7.y, 100f, rect7.height);
        Widgets.CustomButtonText(ref rect8, "ApparelTab.GUI.ArmorHeat".Translate(), Utilities.BgColorButton,
            Color.white, Color.white);
        Button(rect8, ref Filter.sortByArmorHeat);
        var rect9 = new Rect(rect8.x + rect8.width + 10f, rect8.y, 100f, rect8.height);
        Widgets.CustomButtonText(ref rect9, "ApparelTab.InsulationCold".Translate(), Utilities.BgColorButton,
            Color.white, Color.white);
        Button(rect9, ref Filter.sortByInsulationCold);
        var rect10 = new Rect(rect9.x + rect9.width + 10f, rect9.y, 100f, rect9.height);
        Widgets.CustomButtonText(ref rect10, "ApparelTab.GUI.InsulationHeat".Translate(), Utilities.BgColorButton,
            Color.white, Color.white);
        Button(rect10, ref Filter.sortByInsulationHeat);
        var rect11 = new Rect(rect10.x + rect10.width + 10f, rect10.y, 100f, rect10.height);
        var text2 = Filter.mod == null ? "ApparelTab.GUI.Null".Translate().ToString() : Filter.mod.Name;
        if (Widgets.CustomButtonText(ref rect11, "ApparelTab.GUI.Mod".Translate() + ":\n" + text2,
                Utilities.BgColorButton, Color.white, Color.white))
        {
            var list3 = new List<FloatMenuOption>();
            foreach (var mod in Utilities.mods)
            {
                list3.Add(new FloatMenuOption(mod.Name, delegate
                {
                    Filter.mod = mod;
                    Filter.FilteredApparels();
                }));
            }

            list3.Add(new FloatMenuOption("ApparelTab.GUI.Null".Translate(), delegate
            {
                Filter.mod = null;
                Filter.FilteredApparels();
            }));
            Find.WindowStack.Add(new FloatMenu(list3));
        }

        Button(rect11, ref Filter.sortByMod);
        var outRect = new Rect(0f, 100f, InitialSize.x - 40f, InitialSize.y - 140f);
        var viewRect = new Rect(0f, 0f, outRect.x, Filter.FilteredApparels().Count * 70);
        var num = 0;
        Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);
        foreach (var filteredApparel in Filter.filteredApparels)
        {
            Text.Anchor = TextAnchor.MiddleCenter;
            var texture2D = filteredApparel.texture2D;
            var rect12 = new Rect(0f, 70 * num, outRect.width - 30f, 70f);
            Widgets.DrawBoxSolidWithOutline(rect12, Utilities.BgColorButton, Color.gray);
            if (Widgets.ButtonInvisible(rect12))
            {
                Find.WindowStack.Add(new Dialog_InfoCard(filteredApparel.def));
            }

            var position = new Rect(rect12.x + 10f, rect12.y + 5f, rect.height, rect12.height - 10f);
            if (texture2D != null)
            {
                UnityEngine.GUI.DrawTexture(position, texture2D);
            }

            var rect13 = new Rect(rect.width + 15f, rect12.y + 5f, rect2.width - 10f, rect12.height - 10f);
            Widgets.Label(rect13, filteredApparel.def.LabelCap);
            var rect14 = new Rect(rect13.x + rect2.width + 10f, rect12.y + 5f, rect4.width - 10f, rect12.height - 10f);
            Widgets.Label(rect14, filteredApparel.techLevel.ToString());
            var rect15 = new Rect(rect14.x + rect4.width + 10f, rect12.y + 5f, rect5.width - 10f, rect12.height - 10f);
            Widgets.Label(rect15,
                filteredApparel.apparelLayerDef.NullOrEmpty()
                    ? "ApparelTab.GUI.Unknown".Translate().ToString()
                    : filteredApparel.apparelLayerDef);
            var rect16 = new Rect(rect15.x + rect5.width + 10f, rect12.y + 5f, rect6.width - 10f, rect12.height - 10f);
            Widgets.Label(rect16, filteredApparel.armorBlunt.ToString());
            var rect17 = new Rect(rect16.x + rect6.width + 10f, rect12.y + 5f, rect7.width - 10f, rect12.height - 10f);
            Widgets.Label(rect17, filteredApparel.armorSharp.ToString());
            var rect18 = new Rect(rect17.x + rect7.width + 10f, rect12.y + 5f, rect8.width - 10f, rect12.height - 10f);
            Widgets.Label(rect18, filteredApparel.armorHeat.ToString());
            var rect19 = new Rect(rect18.x + rect8.width + 10f, rect12.y + 5f, rect9.width - 10f, rect12.height - 10f);
            Widgets.Label(rect19, filteredApparel.insulation_Cold.ToString());
            var rect20 = new Rect(rect19.x + rect9.width + 10f, rect12.y + 5f, rect10.width - 10f, rect12.height - 10f);
            Widgets.Label(rect20, filteredApparel.insulation_Heat.ToString());
            Widgets.Label(
                new Rect(rect20.x + rect10.width + 10f, rect12.y + 5f, rect11.width - 10f, rect12.height - 10f),
                filteredApparel.mod);
            Text.Anchor = TextAnchor.UpperLeft;
            num++;
        }

        Widgets.EndScrollView();
    }

    public void ButtonClick(ref bool? Bool, bool Bool2)
    {
        if (Bool != Bool2)
        {
            Bool = Bool2;
            Filter.FilteredApparels();
        }
        else
        {
            Bool = null;
        }

        Filter.FilteredApparels();
    }

    public void Button(Rect rect, ref bool? Bool)
    {
        var rect2 = new Rect(rect.x, rect.y + rect.height + 5f, (rect.width / 2f) - 5f, 30f);
        if (Widgets.CustomButtonText(ref rect2, "▼", Utilities.BgColorButton, Color.white,
                Bool == true ? Color.white : Color.gray))
        {
            ButtonClick(ref Bool, true);
        }

        var rect3 = new Rect(rect2.x + rect2.width + 10f, rect2.y, rect2.width, rect2.height);
        if (Widgets.CustomButtonText(ref rect3, "▲", Utilities.BgColorButton, Color.white,
                Bool == false ? Color.white : Color.gray))
        {
            ButtonClick(ref Bool, false);
        }
    }
}