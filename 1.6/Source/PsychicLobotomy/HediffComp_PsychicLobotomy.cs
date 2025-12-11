using System.Collections.Generic;
using Verse;

namespace OOFlavorPack {
    public class HediffComp_PsychicLobotomy : HediffComp
    {
        public HediffCompProperties_PsychicLobotomy Props => 
            (HediffCompProperties_PsychicLobotomy)props;
        public bool Blocks(HediffDef incoming)
        {
            return Props.blockedHediffs.Contains(incoming);
        }
    }
    public class HediffCompProperties_PsychicLobotomy : HediffCompProperties
    {
        public List<HediffDef> blockedHediffs;

        public HediffCompProperties_PsychicLobotomy()
        {
            this.compClass = typeof(HediffComp_PsychicLobotomy);
        }
    }
}