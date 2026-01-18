using System.Collections.Generic;
using Verse;

namespace ExtraAnomalies
{
    public class HediffCompProperties_RequireOtherHediff : HediffCompProperties
    {
        public HediffCompProperties_RequireOtherHediff()
        {
            compClass = typeof(HediffComp_RequireOtherHediff);
        }
        public float decayAmountPerHour;
        public List<HediffDef> hediffTypes;
    }
}