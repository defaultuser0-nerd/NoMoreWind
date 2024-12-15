using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace NoMoreWind
{
    [HarmonyPatch(typeof(ForceVolume))]
    public static class Patches
    {
        public static List<ForceVolume> ActiveForces = [];

        [HarmonyPatch(nameof(ForceVolume.OnTriggerEnter)), HarmonyPrefix]
        public static bool ForceEnter(ForceVolume __instance, Collider other)
        {
            if (other.gameObject == GorillaTagger.Instance.headCollider.gameObject)
            {
                ActiveForces.AddIfNew(__instance); // Gorilla Tag extension method - won't work for other workspaces unless you incorperate the extension
                return Plugin.UseForceMethods;
            }
            return true;
        }

        [HarmonyPatch(nameof(ForceVolume.OnTriggerStay)), HarmonyPrefix]
        public static bool ForceStay(ForceVolume __instance, Collider other)
        {
            if (other.gameObject == GorillaTagger.Instance.headCollider.gameObject)
            {
                if (ActiveForces.Contains(__instance)) ActiveForces.Remove(__instance);
                return Plugin.UseForceMethods;
            }
            return true;
        }

        [HarmonyPatch(nameof(ForceVolume.OnTriggerExit)), HarmonyPrefix]
        public static bool ForceExit(ForceVolume __instance, Collider other)
        {
            if (other.gameObject == GorillaTagger.Instance.headCollider.gameObject)
            {
                return Plugin.UseForceMethods;
            }
            return true;
        }
    }
}
