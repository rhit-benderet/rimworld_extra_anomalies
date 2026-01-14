using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
	public class HediffCompProperties_Transformation : HediffCompProperties
	{
		public HediffCompProperties_Transformation()
		{
			this.compClass = typeof(HediffComp_Transformation);
		} 
        
        public List<PawnKindDef> transformIntoDef;
		public float revealTransformSeverity;
	}
}