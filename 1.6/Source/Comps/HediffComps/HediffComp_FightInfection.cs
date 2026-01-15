using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace ExtraAnomalies
{
	public class HediffComp_FightInfection : HediffComp
	{
		public HediffCompProperties_FightInfection Props
		{
			get
			{
				return (HediffCompProperties_FightInfection)this.props;
			}
		}
		public override void CompPostTick(ref float severityAdjustment)
		{
			base.CompPostTick(ref severityAdjustment);
			if (Find.TickManager.TicksGame % 200 == 0)
            {
                Pawn pawn = this.parent.pawn;
                List<Hediff> hediffs = new List<Hediff>();
                pawn.health.hediffSet.GetHediffs<Hediff>(ref hediffs, hediff =>
                {
                    if (hediff.def.Equals(this.Props.infectionDef))
                    {
                        if (hediff.TryGetComp<HediffComp_SeverityPerDay>() != null)
                        {
                            return true;
                        }
                    }
                    return false;
                });
                float maxSeverity = -0.99f;
                foreach (Hediff hediff in hediffs)
                {
                    float currentSeverity = hediff.Severity;
                    float relevantBarrier = 0.0f;
                    for (int i = 0; i < this.Props.severityBarriers.Count; i++)
                    {
                        if (currentSeverity >= this.Props.severityBarriers[i] && this.Props.severityBarriers[i] > relevantBarrier)
                        {
                            relevantBarrier = this.Props.severityBarriers[i];
                        }
                    }
                    float totalSeverityPerDay = this.Props.severityPerDay + hediff.TryGetComp<HediffComp_SeverityPerDay>().severityPerDay;
                    float newSeverity = hediff.Severity - totalSeverityPerDay / 300f;
                    if (newSeverity <= relevantBarrier)
                    {
                        hediff.Severity = relevantBarrier;
                    } else
                    {
                        hediff.Severity = newSeverity;
                    }
                    if (hediff.Severity > maxSeverity)
                    {
                        maxSeverity = hediff.Severity;
                    }
                }
                this.parent.Severity = maxSeverity + 1f;
            }
		}
	}
}
