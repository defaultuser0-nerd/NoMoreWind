using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using Newtilla;

namespace NoMoreWind
{
    [BepInPlugin(Constants.Guid, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static bool UseForceMethods = true;
        // TRUE: Forces act per-usual
        // FALSE: Forces are disabled, any forces used by the player will ceist to function

        private bool inModdedRoom;

        public void Awake()
        {
            Harmony.CreateAndPatchAll(GetType().Assembly, Constants.Guid); // Not listed under Harmony docs (https://harmony.pardeike.net/articles/basics.html) but this does mostly the same code under "Patching using annotations"

            Newtilla.Newtilla.OnJoinModded += OnModdedJoin;
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeave;
            // TillaHook.TillaHook.OnModdedJoin += OnModdedJoin; - TillaHook isnt out... yet
            // TillaHook.TillaHook.OnModdedLeave += OnModdedLeave;
        }

        public void OnEnable()
        {
            Logger?.LogInfo("OnEnable");
            SetForces(!inModdedRoom); // We preferably want forces to not affect the player - modified code
        }

        public void OnDisable()
        {
            Logger?.LogInfo("OnDisable");
            SetForces(true); // We want forces to affect the player - typical code
        }

        public void OnModdedJoin(string gameMode)
        {
            Logger?.LogInfo("OnModdedJoin");
            inModdedRoom = true;
            SetForces(!enabled); // We preferably want forces to not affect the player - modified code
            // The "enabled" variable is inherited from the "Behaviour" class
        }

        public void OnModdedLeave(string gameMode)
        {
            Logger?.LogInfo("OnModdedLeave");
            inModdedRoom = false;
            SetForces(true); // We want forces to affect the player - typical code
        }

        public void SetForces(bool useForces)
        {
            Logger?.LogInfo($"SetForces ({useForces})");

            if (!useForces)
            {
                List<ForceVolume> activeForces = new(Patches.ActiveForces); // Create a NEW LIST from our EXISTING LIST, the latter will be modified based on what we'll do to the forces shortly
                foreach(ForceVolume force in activeForces)
                {
                    force.OnTriggerExit(GorillaTagger.Instance.headCollider); // Have our active force believe the player has exit it's trigger
                }
            }

            UseForceMethods = useForces;
        }
    }
}
