using Verse;

namespace ExtraAnomalies
{
    public class CompProperties_MonitorEscapeTimer : CompProperties
    {
        public CompProperties_MonitorEscapeTimer()
        {
            compClass = typeof(Comp_MonitorEscapeTimer);
        }
        public float daysToEscape;
    }
}