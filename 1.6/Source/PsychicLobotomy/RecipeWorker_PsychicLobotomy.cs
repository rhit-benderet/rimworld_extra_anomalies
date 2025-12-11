using System.Collections.Generic;
using RimWorld;
using Verse;

namespace OOFlavorPack
{
    public class RecipeWorker_PsychicLobotomy : Recipe_Surgery
    {
        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            return new BodyPartRecord[] { pawn.health.hediffSet.GetBrain() };
        }
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (thing is Pawn pawn)
            {
                if (!pawn.health.hediffSet.HasHediff(HediffDefOf.PsychicAmplifier))
                {
                    return false;
                }
                if (pawn.health.hediffSet.HasHediff(ModHediffDefs.PsychicLobotomy))
                {
                    return false;
                }
                if (!LoadedModManager.GetMod<OOFlavorPackMod>().GetSettings<Settings>().psylinkRemoval)
                {
                    return false;
                }
                return base.AvailableOnNow(thing, part);
            }
            return false;
        }
        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (billDoer != null)
            {
                if (base.CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    return;
                }
                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, new object[]
                {
                    billDoer,
                    pawn
                });
            }
            foreach (Ability ability in pawn.abilities.AllAbilitiesForReading)
            {
                if (ability.def.IsPsycast)
                {
                    pawn.abilities.RemoveAbility(ability.def);
                }
            }
            if (ModLister.GetActiveModWithIdentifier("VanillaExpanded.VPsycastsE") != null)
            {
                ResetVPEPsycasts.Reset(pawn);
            }
            pawn.health.AddHediff(ModHediffDefs.PsychicLobotomy, part, null, null);
            Hediff hediff = pawn.health.hediffSet.hediffs.Find((Hediff x) => x.def == HediffDefOf.PsychicAmplifier && x.Part == part);
            if (hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
            }
            if (ModLister.GetActiveModWithIdentifier("VanillaExpanded.VPsycastsE") != null)
            {
                Hediff hediff2 = pawn.health.hediffSet.hediffs.Find((Hediff x) => x.def == HediffDef.Named("VPE_PsycastAbilityImplant") && x.Part == part);
                if (hediff2 != null)
                {
                    pawn.health.RemoveHediff(hediff2);
                }
            }
            this.OnSurgerySuccess(pawn, part, billDoer, ingredients, bill);
            if (this.IsViolationOnPawn(pawn, part, Faction.OfPlayerSilentFail))
            {
                base.ReportViolation(pawn, billDoer, pawn.HomeFaction, -70, null);
            }
        }
    }
}