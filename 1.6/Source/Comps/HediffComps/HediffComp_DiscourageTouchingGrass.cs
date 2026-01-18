using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
    public class HediffComp_DiscourageTouchingGrass : HediffComp
    {
        public HediffCompProperties_DiscourageTouchingGrass Props => (HediffCompProperties_DiscourageTouchingGrass)this.props;
        
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (Find.TickManager.TicksGame % 100 == 0)
            {
                Pawn pawn = this.parent.pawn;
                Map map = pawn.Map;
                bool isOutdoors = pawn.GetRoom(RegionType.Set_All).TouchesMapEdge;
                float change = isOutdoors ? this.Props.severityPerHourOutside : this.Props.severityPerHourInside;
                List<Hediff> hediffs = new List<Hediff>();
                pawn.health.hediffSet.GetHediffs<Hediff>(ref hediffs, hediff =>
                {
                    if (hediff.def.Equals(this.Props.hediffDef))
                    {
                        return true;
                    }
                    return false;
                });
                if (hediffs.Count == 0 && change > 0f)
                {
                    pawn.health.AddHediff(this.Props.hediffDef);
                }
                for (int i = 0; i < hediffs.Count; i++)
                {
                    float severity = hediffs[i].Severity + change * 0.04f;
                    if (severity > this.Props.maxSeverity)
                    {
                        severity = this.Props.maxSeverity;
                    }
                    hediffs[i].Severity = severity;
                }
            }
        }
        
    }
}