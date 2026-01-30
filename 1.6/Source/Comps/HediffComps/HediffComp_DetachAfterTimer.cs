using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace ExtraAnomalies
{
    public class HediffComp_DetachAfterTimer : HediffComp
    {
        public float timeOnPawn;
        public float studyAmount;
        public HediffCompProperties_DetachAfterTimer Props => (HediffCompProperties_DetachAfterTimer)this.props;
        
        private float TicksToDetach => this.Props.daysToDetach * 60000f;
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (Find.TickManager.TicksGame % 250 == 0)
            {
                this.timeOnPawn += 250f;
                if (this.timeOnPawn >= TicksToDetach)
                {
                    Map map = this.parent.pawn.Map;
                    Thing thing = ThingMaker.MakeThing(this.Props.spawnedItem);
                    if (thing.HasComp<CompStudiable>())
                    {
                        CompStudiable comp = thing.TryGetComp<CompStudiable>();
                        comp.anomalyKnowledgeGained = this.studyAmount;
                    }
                    this.parent.pawn.health.RemoveHediff(this.parent);
                    GenSpawn.Spawn(thing, this.parent.pawn.Position, map, WipeMode.Vanish);

                }
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look(ref this.timeOnPawn, "timeOnPawn", 0f);
            Scribe_Values.Look(ref this.studyAmount, "studyAmount", 0f);
        }
        private int TicksUntilDetach
        {
            get
            {
                return (int)(this.TicksToDetach - this.timeOnPawn);
            }
        }
        public override string CompTipStringExtra
		{
			get
			{
                int ticksLeft = this.TicksUntilDetach;
                string time = ticksLeft.ToStringTicksToPeriod(true, false, true, true, false);
				
				return "ExtraAnomalies.DetachAfterTimer".Translate(time);
			}
		}
        
    }
}