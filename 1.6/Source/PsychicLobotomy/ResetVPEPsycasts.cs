using VanillaPsycastsExpanded;
using Verse;

namespace OOFlavorPack
{
    class ResetVPEPsycasts
    {
        static public void Reset(Pawn pawn)
        {
            Hediff_PsycastAbilities hediff_PsycastAbilities = pawn.Psycasts();
                if (hediff_PsycastAbilities != null)
                {
                    hediff_PsycastAbilities.Reset();
                }
            
        }
    }
}