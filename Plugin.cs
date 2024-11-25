using System;
using BepInEx;
using UnityEngine;
using Newtilla;
using NoMoreWind.Patches;
namespace nomorewind
   {
        [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
	bool inRoom;

	void Start()
	{
            Newtilla.Newtilla.OnJoinModded += OnModdedJoined;
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeft;
        }

	void OnEnable()
	{
	   HarmonyPatches.ApplyHarmonyPatches();
	}

	void OnDisable()
	{
	   HarmonyPatches.RemoveHarmonyPatches();
	}

	void OnGameInitialized(object sender, EventArgs e)
	{
        }

	void Update()
	{
        }

        void OnModdedJoined(string modeName)
        {
	    GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(false);

            inRoom = true;
	}

        void OnModdedLeft(string modeName)
        {
	    GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(true);

            inRoom = false;
        }
      } 
   }
