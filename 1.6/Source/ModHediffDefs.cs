using Verse;

namespace OOFlavorPack
{
    [StaticConstructorOnStartup]
    public static class ModHediffDefs
    {
        public static readonly HediffDef PsychicLobotomy;

        static ModHediffDefs()
        {
            PsychicLobotomy = HediffDef.Named("OOFlavorPack_PsychicLobotomy");
        }
    }
}