using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
	public class HediffCompProperties_FightInfection : HediffCompProperties
	{
		public HediffCompProperties_FightInfection()
		{
			this.compClass = typeof(HediffComp_FightInfection);
		} 
        
        public HediffDef infectionDef;
        public List<float> severityBarriers;
		public float severityPerDay;
	}
}