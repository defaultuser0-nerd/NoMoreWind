using System;
using BepInEx;
using UnityEngine;
using NoMoreWind.Patches;
namespace nomorewind
   {
        [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
	bool inRoom;

	void Start()
	{

    }

	void OnEnable()
	{
	   HarmonyPatches.ApplyHarmonyPatches();
	}

	void OnDisable()
	{
	   HarmonyPatches.RemoveHarmonyPatches();
	}

	

	void Update()
	{
			if (PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED")) OnJoin();
			if(!PhotonNetwork.InRoom) OnLeave();
    }

        void OnJoin()
        {
	    GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(false);

            inRoom = true;
	}

        void OnLeave()
        {
	    GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(true);

            inRoom = false;
        }
      } 
   }

