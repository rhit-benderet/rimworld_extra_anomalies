using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace ExtraAnomalies
{
    public class Comp_AnkleMonitorAttachSelf : ThingComp
    {
        public CompProperties_AnkleMonitorAttachSelf Props {
            get {
                return (CompProperties_AnkleMonitorAttachSelf)this.props;
            }
        }
        private bool ValidatePawn(Pawn pawn)
		{
            foreach (HediffDef def in HediffGroups.ankleMonitorHediffs)
            {
                if (pawn.health.hediffSet.HasHediff(def, false))
                {
                    return false;
                }   
            }
            List<BodyPartRecord> legs = pawn.health.hediffSet.GetNotMissingParts().Where(x => x.def == BodyPartDefOf.Leg).ToList();
			if (legs.Count == 0)
            {
                return false;
            }
            if (pawn.MapHeld != this.parent.MapHeld)
			{
				return false;
			}
			if (this.parent.IsInCaravan())
			{
				return false;
			}
			return true;
		}
        public override void CompTickInterval(int delta)
		{
			base.CompTickInterval(delta);
			if (!this.parent.IsHashIntervalTick(2500, delta))
			{
				return;
			}
			Pawn pawn;
			if (Rand.MTBEventOccurs(3f, 60000f, 2500f) && QuestUtility.TryGetIdealColonist(out pawn, this.parent.MapHeld, new Func<Pawn, bool>(this.ValidatePawn)))
			{
				this.AttachTo(pawn);
			}
		}
        private void AttachTo(Pawn pawn)
        {
            string objectname = this.parent.GetCustomLabelNoCount();
            Thing monitor = this.parent;
            SendLetterForAttach(pawn.NameShortColored, pawn, objectname);
            List<BodyPartRecord> legs = pawn.health.hediffSet.GetNotMissingParts().Where(x => x.def == BodyPartDefOf.Leg).ToList();
			Hediff hediff_monitor = pawn.health.AddHediff(EAHediff_Def.Hediff_EAAnkleMonitor, legs.RandomElement());
			if (hediff_monitor.TryGetComp<HediffComp_DetachAfterTimer>() != null)
			{
				if (monitor.TryGetComp<CompStudiable>() != null)
				{
					CompStudiable comp = monitor.TryGetComp<CompStudiable>();
					hediff_monitor.TryGetComp<HediffComp_DetachAfterTimer>().studyAmount = comp.anomalyKnowledgeGained;
				}
			}
            this.parent.Destroy(DestroyMode.Vanish);
        }
        public static void SendLetterForAttach(NamedArgument attacheeName, Thing attachee, NamedArgument monitor)
        {
            TaggedString title = "ExtraAnomalies.RandomAttachment".Translate(monitor).CapitalizeFirst();
            TaggedString letterText = "ExtraAnomalies.RandomAttachment.Letter".Translate(attacheeName, monitor);

            Find.LetterStack.ReceiveLetter(title, letterText, LetterDefOf.NegativeEvent, new TargetInfo(attachee));
        }
    }
}