using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
    public class HediffComp_DetachAfterDeath : HediffComp
    {
        public HediffCompProperties_DetachAfterDeath Props => (HediffCompProperties_DetachAfterDeath)this.props;
        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            base.Notify_PawnDied(dinfo, culprit);
            Map map = this.parent.pawn.MapHeld;
            Thing thing = ThingMaker.MakeThing(this.Props.spawnedItem);
            if (this.parent.GetComp<HediffComp_DetachAfterTimer>() != null)
            {
                HediffComp_DetachAfterTimer detach = this.parent.GetComp<HediffComp_DetachAfterTimer>();
                if (thing.HasComp<CompStudiable>())
                {
                    CompStudiable comp = thing.TryGetComp<CompStudiable>();
                    comp.anomalyKnowledgeGained = detach.studyAmount;
                }
            }
            this.parent.pawn.health.RemoveHediff(this.parent);
            GenSpawn.Spawn(thing, this.parent.pawn.Position, map, WipeMode.Vanish);
        }
    }
}