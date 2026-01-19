using Verse;

namespace ExtraAnomalies
{
    public class CompProperties_SyntheticLeg : CompProperties
    {
        public CompProperties_SyntheticLeg()
        {
            compClass = typeof(Comp_SyntheticLeg);
        }
        public ThingDef turnIntoOnceAttached;
    }
}