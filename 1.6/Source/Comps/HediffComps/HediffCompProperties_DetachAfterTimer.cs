using Verse;

namespace ExtraAnomalies
{
    public class HediffCompProperties_DetachAfterTimer : HediffCompProperties
    {
        public HediffCompProperties_DetachAfterTimer()
        {
            compClass = typeof(HediffComp_DetachAfterTimer);
        }

        public float daysToDetach;
        public ThingDef spawnedItem;
    }
}