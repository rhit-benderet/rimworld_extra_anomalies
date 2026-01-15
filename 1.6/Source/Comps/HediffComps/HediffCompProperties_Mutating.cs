using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
	public class HediffCompProperties_Mutating : HediffCompProperties
	{
		public HediffCompProperties_Mutating()
		{
			this.compClass = typeof(HediffComp_Mutating);
		} 
        
        public List<float> mutationSeverities;
	}
}