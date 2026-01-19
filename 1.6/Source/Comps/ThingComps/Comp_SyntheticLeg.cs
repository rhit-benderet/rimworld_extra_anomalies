using Verse;

namespace ExtraAnomalies
{
    public class Comp_SyntheticLeg : ThingComp
    {
         public CompProperties_SyntheticLeg Props
		{
			get
			{
				return (CompProperties_SyntheticLeg)this.props;
			}
		}
    }
}