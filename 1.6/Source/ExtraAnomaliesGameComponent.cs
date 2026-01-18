using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace ExtraAnomalies
{
    public class ExtraAnomaliesGameComponent : GameComponent
    {
        public static ExtraAnomaliesGameComponent instance;

        public ExtraAnomaliesGameComponent(Game game)
        {
            instance = this;
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }
    }
}