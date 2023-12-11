using HarmonyLib;
using TMPro;
using UnityEngine;

namespace DisplaySurvivalTimeOnDeath.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class HUDManagerPatch
    {
        public static HUDManagerPatch Instance;
        public static GameObject SurvivalTime = null;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

        }

        [HarmonyPatch("UpdateBoxesSpectateUI")]
        [HarmonyPostfix]
        static void showSurvivalTimeOnHUD()
        {
            if (HUDManagerPatch.SurvivalTime == null)
            {
                GameObject Spectating = GameObject.Find("Spectating");
                SurvivalTime = UnityEngine.Object.Instantiate(Spectating, Spectating.transform.parent);

                SurvivalTime.name = "Survival Time";
                SurvivalTime.GetComponent<TextMeshProUGUI>().transform.Translate(new Vector3(0, -0.2f, 0));
            }

            SurvivalTime.GetComponent<TextMeshProUGUI>().text = "You survived for " + PlayerControllerBPatch.survivalTime;
        }
    }
}