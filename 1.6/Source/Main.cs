using System;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace OOFlavorPack
{
    public class OOFlavorPackMod : Mod
	{
		Settings settings;
        public OOFlavorPackMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<Settings>();
            var harmony = new Harmony("ooflavorpack");
            harmony.PatchAll();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("OO.enablePsylinkRemoval".Translate(), ref settings.psylinkRemoval, "OO.enablePsylinkRemovalTooltip".Translate());
            listingStandard.Label("OO.enablePsylinkRemovalExplanation".Translate());
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory()
        {
            return "OOFlavorPack".Translate();
        }
	}
    [HarmonyPatch(typeof(Pawn_HealthTracker), "AddHediff", 
    new Type[] { typeof(Hediff), typeof(BodyPartRecord), typeof(DamageInfo?) , typeof(DamageWorker.DamageResult) })]
    public static class Patch_BlockByComp
    {
        static bool Prefix(Pawn_HealthTracker __instance, Hediff hediff, BodyPartRecord part = null, DamageInfo? dinfo = null, DamageWorker.DamageResult result = null)
        {
            Pawn pawn = (Pawn)AccessTools.Field(__instance.GetType(), "pawn")?.GetValue(__instance);
            foreach (var h in pawn.health.hediffSet.hediffs)
            {
                var comp = h.TryGetComp<HediffComp_PsychicLobotomy>();
                if (comp != null && comp.Blocks(hediff.def))
                {
                    return false;
                }
            }

            return true;
        }
    }
}