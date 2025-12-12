using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace OOFlavorPack
{
	public class PsychicRitualDef_PsylinkSkipAbductionPlayer : PsychicRitualDef_InvocationCircle
	{
		public override List<PsychicRitualToil> CreateToils(PsychicRitual psychicRitual, PsychicRitualGraph parent)
		{
			List<PsychicRitualToil> list = base.CreateToils(psychicRitual, parent);
			list.Add(new PsychicRitualToil_PsylinkSkipAbductionPlayer(this.InvokerRole));
			return list;
		}

		public override TaggedString OutcomeDescription(FloatRange qualityRange, string qualityNumber, PsychicRitualRoleAssignments assignments)
		{
			string value = Mathf.FloorToInt(this.comaDurationDaysFromQualityCurve.Evaluate(qualityRange.min) * 60000f).ToStringTicksToDays("F1");
            string psylinkfavvalue = Mathf.FloorToInt(this.psylinkFavorabilityFromQualityCurve.Evaluate(qualityRange.min) * 100f).ToString() + "%";
			return this.outcomeDescription.Formatted(value, psylinkfavvalue);
		}
		public SimpleCurve comaDurationDaysFromQualityCurve;
		public SimpleCurve psylinkFavorabilityFromQualityCurve;
	}
}
