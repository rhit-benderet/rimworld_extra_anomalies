using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
	public class CompTargetableProperties_AnkleMonitorTarget : CompProperties_Targetable
	{
		public CompTargetableProperties_AnkleMonitorTarget()
		{
			compClass = typeof(CompTargetable_AnkleMonitorTarget);
		}
		public bool canPutOnSyntheticLeg;
	}
}
