using System;
using System.Collections.Generic;
using System.Linq;
using AnomaliesExpected;
using RimWorld;
using Verse;


namespace ExtraAnomalies
{
	public class Comp_AnomalousSyringe : CompInteractable
	{
		 public new CompProperties_Interactable Props => (CompProperties_Interactable)props;
        protected CompAEStudyUnlocks StudyUnlocks => studyUnlocksCached ?? (studyUnlocksCached = parent.TryGetComp<CompAEStudyUnlocks>());
        private CompAEStudyUnlocks studyUnlocksCached;

        public override bool HideInteraction => (StudyUnlocks?.NextIndex ?? 1) == 0;

        private void GiveHediff(Pawn pawn)
        {
            float Severity = 0.01f;
            if (pawn.health.hediffSet.HasHediff(EAHediff_Def.Hediff_EAAnomalousSyringe))
            {
                Severity = 1f;
            }
            HealthUtility.AdjustSeverity(pawn, EAHediff_Def.Hediff_EAAnomalousSyringe, Severity);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                if (gizmo is Command_Action command_Action)
                {
                    command_Action.hotKey = KeyBindingDefOf.Misc6;
                }
                yield return gizmo;
            }
        }

        protected override void OnInteracted(Pawn caster)
        {
            GiveHediff(caster);
        }
    }
}
