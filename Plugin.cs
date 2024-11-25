using BepInEx;
using Photon.Pun;
using System;
using UnityEngine;
namespace nomorewind
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]

    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        void Start()
        {

        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            inRoom = PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED");
        }

        void Update()
        {
            if (inRoom)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(false);
            }
            else
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(true);
            }
        }
    }
}
