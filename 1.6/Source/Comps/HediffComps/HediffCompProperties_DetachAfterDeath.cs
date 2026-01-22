using Verse;

namespace ExtraAnomalies
{
    public class HediffCompProperties_DetachAfterDeath : HediffCompProperties
    {
        public HediffCompProperties_DetachAfterDeath()
        {
            compClass = typeof(HediffComp_DetachAfterDeath);
        }
        public ThingDef spawnedItem;
    }
}