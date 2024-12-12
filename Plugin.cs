using System;
using BepInEx;
using UnityEngine;
using Utilla;
using Newtilla;
using NoMoreWind.Patches;
namespace nomorewind
{
    [ModdedGamemode]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        void Start()
        {
            Newtilla.Newtilla.OnJoinModded += OnModdedJoined;
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeft;
            HarmonyPatches.ApplyHarmonyPatches();
        }

        [ModdedGamemodeJoin]
        void OnModdedJoined(string modeName)
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(false);
            Debug.Log("Wind removed");
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        void OnModdedLeft(string modeName)
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(true);
            Debug.Log("Wind readded");
            inRoom = false;
        }
    }
}
