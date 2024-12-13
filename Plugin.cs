using System;
using BepInEx;
using UnityEngine;
using Utilla;
using Newtilla;
using NoMoreWind.Patches;
namespace nomorewind
{
    [ModdedGamemode] // for utilla support
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        void Start()
        {
            Newtilla.Newtilla.OnJoinModded += OnModdedJoined; // for newtilla support
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeft;
            HarmonyPatches.ApplyHarmonyPatches();
        }

        [ModdedGamemodeJoin]
        void OnModdedJoined(string modeName)
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(false); // disables wind
            Debug.Log("Wind removed"); // logs it
            inRoom = true; // iirc only here for utilla
        }

        [ModdedGamemodeLeave]
        void OnModdedLeft(string modeName)
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(true); // enables wind again
            Debug.Log("Wind readded"); // logs it
            inRoom = false; // iirc only here for utilla
        }
    }
}
