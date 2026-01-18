using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace ExtraAnomalies
{
	public class JobDriver_AttachAnkleMonitor : JobDriver
	{
		private Pawn Attachee
		{
			get
			{
				return (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
			}
		}
		private Thing Item
		{
			get
			{
				return this.job.GetTarget(TargetIndex.B).Thing;
			}
		}

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return this.pawn.Reserve(this.Attachee, this.job, 1, -1, null, errorOnFailed, false) && this.pawn.Reserve(this.Item, this.job, 1, -1, null, errorOnFailed, false);
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch, false).FailOnDespawnedOrNull(TargetIndex.B).FailOnDespawnedOrNull(TargetIndex.A);
			yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, false, false, true, false);
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch, false).FailOnDespawnedOrNull(TargetIndex.A);
			Toil toil = Toils_General.Wait(600, TargetIndex.None);
			toil.WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
			toil.FailOnDespawnedOrNull(TargetIndex.A);
			toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
			toil.tickAction = delegate()
			{
				CompUsable compUsable = this.Item.TryGetComp<CompUsable>();
				if (compUsable != null && this.warmupMote == null && compUsable.Props.warmupMote != null)
				{
					this.warmupMote = MoteMaker.MakeAttachedOverlay(this.Attachee, compUsable.Props.warmupMote, Vector3.zero, 1f, -1f);
				}
				Mote mote = this.warmupMote;
				if (mote == null)
				{
					return;
				}
				mote.Maintain();
			};
			yield return toil;
			yield return Toils_General.Do(new Action(this.Attach));
			yield break;
		}

		private void Attach()
		{
			if (this.Attachee.health.hediffSet.HasHediff(EAHediff_Def.Hediff_EAAnkleMonitor, false))
			{
				return;
			}
			List<BodyPartRecord> legs = this.Attachee.health.hediffSet.GetNotMissingParts().Where(x => x.def == BodyPartDefOf.Leg).ToList();
			if (legs.Count == 0)
			{
				return;
			}
			this.Attachee.health.AddHediff(EAHediff_Def.Hediff_EAAnkleMonitor, legs.RandomElement());
			this.Item.SplitOff(1).Destroy(DestroyMode.Vanish);
		}
		private Mote warmupMote;
	}
}
