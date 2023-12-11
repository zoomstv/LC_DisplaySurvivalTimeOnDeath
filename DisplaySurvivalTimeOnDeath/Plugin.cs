using BepInEx;
using BepInEx.Logging;
using DisplaySurvivalTimeOnDeath.Patches;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplaySurvivalTimeOnDeath
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class PluginBase : BaseUnityPlugin
    {
        private const string modGUID = "zoomstv.DisplaySurvivalTimeOnDeath";
        private const string modName = "DisplaySurvivalTimeOnDeath";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static PluginBase Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("DisplaySurvivalTimeOnDeath has awakened");

            harmony.PatchAll(typeof(PluginBase));
            harmony.PatchAll(typeof(HUDManagerPatch));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }
    }
}
