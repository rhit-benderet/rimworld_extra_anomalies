using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace ExtraAnomalies
{
    public class AnkleMonitorTargetingParameters : TargetingParameters
    {
        public AnkleMonitorTargetingParameters()
        {
            canTargetPawns = true;
            canTargetBuildings = false;
            canTargetAnimals = false;
            canTargetMechs = false;
            canTargetEntities = false;
            canTargetItems = false;
            validator = delegate(TargetInfo target)
            {
                return this.ValidateTarget((LocalTargetInfo)target, false);
            };
        }
        public bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
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
            return true;
        }
    }
}