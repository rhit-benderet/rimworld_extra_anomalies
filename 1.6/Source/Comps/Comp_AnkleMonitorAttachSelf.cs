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
			if (pawn.health.hediffSet.HasHediff(EAHediff_Def.Hediff_EAAnkleMonitor, false))
			{
				return false;
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
            SendLetterForAttach(pawn.NameShortColored, pawn, objectname);
            this.parent.Destroy(DestroyMode.Vanish);
            List<BodyPartRecord> legs = pawn.health.hediffSet.GetNotMissingParts().Where(x => x.def == BodyPartDefOf.Leg).ToList();
			pawn.health.AddHediff(EAHediff_Def.Hediff_EAAnkleMonitor, legs.RandomElement());
        }
        public static void SendLetterForAttach(NamedArgument attacheeName, Thing attachee, NamedArgument monitor)
        {
            TaggedString title = "ExtraAnomalies.RandomAttachment".Translate(monitor).CapitalizeFirst();
            TaggedString letterText = "ExtraAnomalies.RandomAttachment.Letter".Translate(attacheeName, monitor);

            Find.LetterStack.ReceiveLetter(title, letterText, LetterDefOf.NegativeEvent, new TargetInfo(attachee));
        }
    }
}