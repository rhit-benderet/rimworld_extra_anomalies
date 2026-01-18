using Verse;

namespace ExtraAnomalies
{
    public class HediffCompProperties_DiscourageTouchingGrass : HediffCompProperties
    {
        public HediffCompProperties_DiscourageTouchingGrass()
        {
            compClass = typeof(HediffComp_DiscourageTouchingGrass);
        }

        public float severityPerHourOutside;
        public float severityPerHourInside;
        public float maxSeverity;
        public HediffDef hediffDef;
    }
}