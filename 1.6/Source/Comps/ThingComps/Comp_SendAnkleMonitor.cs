using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace ExtraAnomalies
{
    public class Comp_SendAnkleMonitor : CompAbilityEffect
    {
         public new CompProperties_SendAnkleMonitor Props
		{
			get	
			{
				return (CompProperties_SendAnkleMonitor)this.props;
			}
		}
		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			Pawn pawn = target.Pawn;
			if (pawn == null)
			{
				return;
			}
			Map map = pawn.Map;
			IEnumerable<Thing> ankleMonitors = map.spawnedThings.Where((Thing x) => x.def == EAThing_Def.EA_AnkleMonitor);
			IEnumerable<Thing> ankleMonitorsOnLegs = map.spawnedThings.Where((Thing x) => x.def == EAThing_Def.EA_SyntheticLegWithMonitor);
			Thing monitor = null;
			if (ankleMonitors.Count() > 0)
			{
				monitor = ankleMonitors.First();
			} else if (ankleMonitorsOnLegs.Count() > 0)
			{
				monitor = ankleMonitorsOnLegs.First();
			}
			if (monitor == null)
			{
				return;
			}
			foreach (HediffDef hediff in HediffGroups.ankleMonitorHediffs)
			{
				if (pawn.health.hediffSet.HasHediff(hediff, false))
				{
					return;
				}
			}
			List<BodyPartRecord> legs = pawn.health.hediffSet.GetNotMissingParts().Where(x => x.def == BodyPartDefOf.Leg).ToList();
			if (legs.Count == 0)
			{
				return;
			}
			pawn.health.AddHediff(EAHediff_Def.Hediff_EAAnkleMonitor, legs.RandomElement());
			monitor.SplitOff(1).Destroy(DestroyMode.Vanish);
			Faction factionToInform = pawn.Faction;
			Faction.OfPlayer.TryAffectGoodwillWith(factionToInform, -40, true, !factionToInform.temporary, HistoryEventDefOf.AttackedMember, new GlobalTargetInfo?(pawn));
			Effecter effecter = EAEffects_Def.EA_AttachEffect.Spawn();
			effecter.Trigger(new TargetInfo(target.Cell, this.parent.pawn.MapHeld, false), new TargetInfo(target.Cell, this.parent.pawn.MapHeld, false), -1);
			effecter.Cleanup();
			
		}
		public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
		{
			return this.Valid(target, false);
		}
		public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
		{
			Pawn pawn = target.Pawn;
			if (pawn == null)
			{
				return false;
			}
			Map map = pawn.Map;
			IEnumerable<Thing> ankleMonitors = map.spawnedThings.Where((Thing x) => x.def == EAThing_Def.EA_AnkleMonitor);
			IEnumerable<Thing> ankleMonitorsOnLegs = map.spawnedThings.Where((Thing x) => x.def == EAThing_Def.EA_SyntheticLegWithMonitor);
			return pawn != null && (ankleMonitorsOnLegs.Count() >= 1 || ankleMonitors.Count() >= 1) && base.Valid(target, throwMessages);
		}
    }
}