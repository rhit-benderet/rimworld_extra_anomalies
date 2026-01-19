using System.Collections.Generic;
using System.ComponentModel;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace ExtraAnomalies
{
    [StaticConstructorOnStartup]
    public class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("ExtraAnomalies.HarmonyPatches");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Thing))]
        [HarmonyPatch(nameof(Thing.PreApplyDamage))]
        static class CancelDamageToAnkleMonitor
        {
            static void Postfix(ref bool absorbed, ref Thing __instance, ref DamageInfo dinfo)
            {
                if (!absorbed && dinfo.HitPart != null)
                {
                    bool isLethal = dinfo.Amount >= __instance.HitPoints;
                    if (isLethal)
                    {
                        if (__instance is Pawn pawn)
                        {
                            IEnumerable<BodyPartRecord> parts = dinfo.HitPart.GetPartAndAllChildParts();
                            foreach (BodyPartRecord part in parts)
                            {
                                if (pawn.health.hediffSet.HasHediff(EAHediff_Def.Hediff_EAAnkleMonitor, part))
                                {
                                    absorbed = true;
                                    if (part == dinfo.HitPart)
                                    {
                                        SendLetterForAbsorption(pawn.NameShortColored, pawn);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void SendLetterForAbsorption(NamedArgument attacheeName, Thing attachee)
        {
            TaggedString title = "ExtraAnomalies.ReattachLeg".Translate();
            TaggedString letterText = "ExtraAnomalies.ReattachLeg.Letter".Translate(attacheeName);

            Find.LetterStack.ReceiveLetter(title, letterText, LetterDefOf.NeutralEvent, new TargetInfo(attachee));
        }
    }
}