using GameNetcodeStuff;
using HarmonyLib;
using System;
using TMPro;
using UnityEngine;

namespace DisplaySurvivalTimeOnDeath.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        public static PlayerControllerBPatch Instance;
        public static string survivalTime = "NULL";

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        [HarmonyPatch("KillPlayer")]
        [HarmonyPrefix]
        public static void ShowSurvivalTimeInDeathScreen()
        {
            GameObject deathScreen = GameObject.Find("DeathScreen").gameObject;

            float timeOfDeath = TimeOfDay.Instance.normalizedTimeOfDay * (60f * TimeOfDay.Instance.numberOfHours) -120; // -120: to begin at 8:00

            float survivalTimeSeconds = (float)(timeOfDeath * 0.71666666); //since 1 ingame Hour is ~43 seconds long

            if (survivalTimeSeconds < 60)
            {
                if (survivalTimeSeconds < 0) //when dying before 8:00
                {
                    survivalTimeSeconds = 0;
                }

                survivalTime = Math.Floor(survivalTimeSeconds).ToString() + " seconds";

            } else
            {
                int minutes = (int)Math.Floor(survivalTimeSeconds / 60);
                int seconds = (int)Math.Floor(survivalTimeSeconds % 60);

                string minutesUnit = "minutes";
                string secondsUnit = "seconds";

                if(minutes == 1)
                {
                    minutesUnit = "minute";
                }
                if (seconds == 1)
                {
                    secondsUnit = "second";
                }

                survivalTime = minutes + " " + minutesUnit + " and " + seconds + " " + secondsUnit;
            }

            deathScreen.GetComponentInChildren<TextMeshProUGUI>().text = "You survived for " + survivalTime;
        }
    }
}