using RimWorld;
using Verse;

namespace ExtraAnomalies
{
    public class Comp_MonitorEscapeTimer : ThingComp
    {
        public CompProperties_MonitorEscapeTimer Props
		{
			get
			{
				return (CompProperties_MonitorEscapeTimer)this.props;
			}
		}
        public int TicksUntilBreach
        {
            get
            {
                return (int)((this.Props.daysToEscape * 60000f) - this.ticksExisted);
            }
        }
        public override string CompInspectStringExtra()
        {
            return "ExtraAnomalies.TicksUntilBreach".Translate(this.TicksUntilBreach.ToStringTicksToPeriod(true, false, true, true, false));
        }
        public int ticksExisted;
        public override void CompTick()
        {
            base.CompTick();
            if (Find.TickManager.TicksGame % 250 == 0) {
                this.ticksExisted += 250;
                if (this.ticksExisted >= (int)(this.Props.daysToEscape * 60000f))
                {
                    Map map = this.parent.Map;
                    IntVec3 pos = this.parent.Position;
                    Thing thing = ThingMaker.MakeThing(EAThing_Def.EA_AnkleMonitor);
                    this.parent.Destroy(DestroyMode.Vanish);
                    GenSpawn.Spawn(thing, pos, map, WipeMode.Vanish);
                }
            }
        }
        
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.ticksExisted, "ticksExisted", 0);
        }
    }
}