using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
	public class CompTargetable_AnkleMonitorTarget : CompTargetable
	{
		protected override bool PlayerChoosesTarget
		{
			get
			{
				return true;
			}
		}
		public new CompTargetableProperties_AnkleMonitorTarget Props => (CompTargetableProperties_AnkleMonitorTarget)base.Props;
        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            Thing thing = target.Thing;
			Pawn pawn = thing as Pawn;
            if (pawn != null)
            {
                List<BodyPartRecord> legs = pawn.health.hediffSet.GetNotMissingParts().Where(x => x.def == BodyPartDefOf.Leg).ToList();
                if (legs.Count == 0)
                {
                    return false;
                }
                foreach (HediffDef def in HediffGroups.ankleMonitorHediffs)
                {
                    if (pawn.health.hediffSet.HasHediff(def, false))
                    {
                        return false;
                    }
                }
            }
			ThingWithComps thingWithComps = thing as ThingWithComps;
            if (thingWithComps != null)
            {
				if (thingWithComps.def.category != ThingCategory.Pawn)
				{
					Comp_SyntheticLeg compSyntheticLeg = thingWithComps.TryGetComp<Comp_SyntheticLeg>();
					if (compSyntheticLeg == null || !this.Props.canPutOnSyntheticLeg)
					{
						return false;
					}
				}	
            }
            return base.ValidateTarget(target, showMessages);
        }
		protected override TargetingParameters GetTargetingParameters()
		{
			return new TargetingParameters
			{
				canTargetPawns = true,
				canTargetBuildings = false,
                canTargetAnimals = false,
                canTargetMechs = false,
                canTargetEntities = false,
				canTargetItems = true,
				thingCategory = ThingCategory.None,
				mapObjectTargetsMustBeAutoAttackable = false,
				validator = delegate(TargetInfo target)
				{
					return this.ValidateTarget((LocalTargetInfo)target, false);
				}
			};
		}
		public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
		{
			yield return targetChosenByPlayer;
			yield break;
		}
	}
}
