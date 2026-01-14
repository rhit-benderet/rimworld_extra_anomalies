using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace ExtraAnomalies
{
	public class HediffComp_Transformation : HediffComp
	{
		public HediffCompProperties_Transformation Props
		{
			get
			{
				return (HediffCompProperties_Transformation)this.props;
			}
		}
        private float DaysUntilTransformation()
        {
            HediffComp_SeverityPerDay severityPerDay = this.parent.TryGetComp<HediffComp_SeverityPerDay>();
            if (severityPerDay != null)
            {
                return (this.parent.def.maxSeverity - this.parent.Severity) / severityPerDay.severityPerDay;
            }
            return -1f;
        }
		public override void CompPostTick(ref float severityAdjustment)
		{
			base.CompPostTick(ref severityAdjustment);
			if (Find.TickManager.TicksGame % 250 == 0 && this.parent.Severity >= this.parent.def.maxSeverity)
			{
				this.Transform();
			}
		}
		protected virtual void Transform()
		{
            Pawn pawn = this.parent.pawn;
            
            PawnKindDef heldPawnKind = this.Props.transformIntoDef.RandomElement();
            Faction ofEntities = Faction.OfEntities;
            PawnGenerationContext context = PawnGenerationContext.NonPlayer;
            float? fixedBiologicalAge = new float?(0f);
            Pawn newPawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(heldPawnKind, ofEntities, context, null, true, false, false, true, false, 1f, false, true, false, true, true, false, false, false, false, 0f, 0f, null, 1f, null, null, null, null, null, fixedBiologicalAge, null, null, null, null, null, null, false, false, false, false, null, null, null, null, null, 0f, DevelopmentalStage.Adult, null, null, null, false, false, false, -1, 0, false));
            
            Find.HiddenItemsManager.SetDiscovered(newPawn.def);
            
            GenSpawn.Spawn(newPawn, pawn.Position, pawn.Map, WipeMode.Vanish);
            pawn.Destroy(DestroyMode.Vanish);
            NamedArgument arg = pawn.NameShortColored;
            SendLetterForTransformation(arg, heldPawnKind.label, this.parent.def.label, newPawn);
            
        }
        public static void SendLetterForTransformation(NamedArgument pawnName, NamedArgument transformedPawnName, NamedArgument hediffName, Thing transformed)
        {
            TaggedString title = "ExtraAnomalies.Transformation".Translate();
            TaggedString letterText = "ExtraAnomalies.Transformation.Transformation_Letter".Translate(pawnName, transformedPawnName, hediffName);

            Find.LetterStack.ReceiveLetter(title, letterText, LetterDefOf.NegativeEvent, new TargetInfo(transformed));
        }
        public override string CompTipStringExtra
		{
			get
			{
                HediffComp_SeverityPerDay severityPerDay = this.parent.TryGetComp<HediffComp_SeverityPerDay>();
				if (severityPerDay != null && this.parent.Severity >= this.Props.revealTransformSeverity)
				{
					return "Days until transformation: " + this.DaysUntilTransformation().ToString("0.0");
				}
				return null;
			}
		}
	}
}
