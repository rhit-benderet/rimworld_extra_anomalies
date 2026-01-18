using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace ExtraAnomalies
{
	public class HediffComp_Mutating : HediffComp
	{
		public HediffCompProperties_Mutating Props
		{
			get
			{
				return (HediffCompProperties_Mutating)this.props;
			}
		}
        private float DaysUntilMutation()
        {
            HediffComp_SeverityPerDay severityPerDay = this.parent.TryGetComp<HediffComp_SeverityPerDay>();
            if (severityPerDay != null)
            {
                float nextMutationSeverity = -1f;
                for (int i = 0; i < this.Props.mutationSeverities.Count; i++)
                {
                    if (this.parent.Severity <= this.Props.mutationSeverities[i] && (nextMutationSeverity == -1f || nextMutationSeverity > this.Props.mutationSeverities[i])) {
                        nextMutationSeverity = this.Props.mutationSeverities[i];
                    }
                }
                if (nextMutationSeverity != -1f) return (nextMutationSeverity - this.parent.Severity) / severityPerDay.severityPerDay;
            }
            return -1f;
        }        
        public override string CompTipStringExtra
		{
			get
			{
                float daysUntilMutation = this.DaysUntilMutation();
				if (daysUntilMutation != -1f)
				{
					return "Days until mutation: " + this.DaysUntilMutation().ToString("0.0");
				}
				return null;
			}
		}
	}
}
