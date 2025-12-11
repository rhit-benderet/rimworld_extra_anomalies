using System.Collections.Generic;
using Verse;
using UnityEngine;

namespace OOFlavorPack
{
    public class Settings : ModSettings
    {
        internal static bool enablePsylinkRemoval;
        public bool psylinkRemoval;
        public override void ExposeData()
        {
            Scribe_Values.Look(ref psylinkRemoval, "psylinkRemoval", false);
            base.ExposeData();
        }
    }
}