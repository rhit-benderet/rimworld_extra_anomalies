using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
    public class HediffComp_RequireOtherHediff : HediffComp
    {
        public HediffCompProperties_RequireOtherHediff Props => (HediffCompProperties_RequireOtherHediff)this.props;
        
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (Find.TickManager.TicksGame % 250 == 0)
            {
                Pawn pawn = this.parent.pawn;
                bool hasRequiredHediff = false;
                foreach (HediffDef hediff in this.Props.hediffTypes)
                {
                    if (pawn.health.hediffSet.HasHediff(hediff))
                    {
                        hasRequiredHediff = true;
                        break;
                    }
                }
                if (!hasRequiredHediff)
                {
                    this.parent.Severity -= this.Props.decayAmountPerHour / 10.0f;
                }
            }
        }
        
    }
}