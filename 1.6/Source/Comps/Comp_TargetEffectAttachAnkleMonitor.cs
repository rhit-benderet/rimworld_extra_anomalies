using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace ExtraAnomalies
{
	public class Comp_TargetEffectAttachAnkleMonitor : CompTargetEffect
	{
		public CompProperties_TargetEffectAttachAnkleMonitor Props
		{
			get
			{
				return (CompProperties_TargetEffectAttachAnkleMonitor)this.props;
			}
		}
		public override void DoEffectOn(Pawn user, Thing target)
		{
			if (!user.IsColonistPlayerControlled)
			{
				return;
			}
			Pawn attachee = (Pawn)target;
			if (attachee.health.hediffSet.HasHediff(EAHediff_Def.Hediff_EAAnkleMonitor, false))
			{
				return;
			}
			List<BodyPartRecord> legs = attachee.health.hediffSet.GetNotMissingParts().Where(x => x.def == BodyPartDefOf.Leg).ToList();
			if (legs.Count == 0)
			{
				return;
			}
			Job job = JobMaker.MakeJob(EAJob_Def.EA_AttachAnkleMonitor, target, this.parent);
			job.count = 1;
			job.playerForced = true;
			user.jobs.TryTakeOrderedJob(job, new JobTag?(JobTag.Misc), false);
		}
	}
}
