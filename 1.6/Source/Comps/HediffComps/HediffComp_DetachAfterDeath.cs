using Verse;

namespace ExtraAnomalies
{
    public class HediffComp_DetachAfterDeath : HediffComp
    {
        public HediffCompProperties_DetachAfterDeath Props => (HediffCompProperties_DetachAfterDeath)this.props;
        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            base.Notify_PawnDied(dinfo, culprit);
            this.parent.pawn.health.RemoveHediff(this.parent);
            Map map = this.parent.pawn.MapHeld;
            Thing thing = ThingMaker.MakeThing(this.Props.spawnedItem);
            GenSpawn.Spawn(thing, this.parent.pawn.Position, map, WipeMode.Vanish);
        }
    }
}