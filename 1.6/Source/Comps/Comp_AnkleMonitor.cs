using System;
using System.Collections.Generic;
using System.Linq;
using AnomaliesExpected;
using RimWorld;
using Verse;
using Verse.Sound;


namespace ExtraAnomalies
{
	public class Comp_AnkleMonitor : CompUsable
	{
        public new CompProperties_Usable Props => (CompProperties_Usable)props;
        protected CompAEStudyUnlocks StudyUnlocks => studyUnlocksCached ?? (studyUnlocksCached = parent.TryGetComp<CompAEStudyUnlocks>());
        private CompAEStudyUnlocks studyUnlocksCached;

        public bool ShouldHide => (StudyUnlocks?.NextIndex ?? 1) == 0;

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (ShouldHide)
            {
                yield break;
            }
            if (!this.Props.showUseGizmo)
            {
                yield break;
            }
            yield return new Command_Action
            {
                icon = this.parent.def.uiIcon,
                defaultLabel = string.Format("{0} {1}...", "ExtraAnomalies.Attach".Translate(), this.parent.def.label),
                defaultDesc = "ExtraAnomalies.AttachTooltip".Translate(this.parent.def.label),
                action = delegate()
                {
                    SoundDefOf.Tick_Tiny.PlayOneShotOnCamera(null);
                    Find.Targeter.BeginTargeting(this, null, false, null, null, true);
                }
            };
            yield break;
        }
        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(Pawn myPawn)
        {
            if (ShouldHide)
            {
                return Enumerable.Empty<FloatMenuOption>();
            }
            return base.CompFloatMenuOptions(myPawn);
        }
    }
}
