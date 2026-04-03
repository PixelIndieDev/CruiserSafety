using BepInEx;
using BepInEx.Logging;
using CruiserSafety.Patches;
using HarmonyLib;

namespace CruiserSafety
{
    [BepInPlugin(ModInfo.modGUID, ModInfo.modName, ModInfo.modVersion)]
    public class CruiserDamagePatchBase : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(ModInfo.modGUID);
        private static CruiserDamagePatchBase instance;

        internal ManualLogSource logSource;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            logSource = BepInEx.Logging.Logger.CreateLogSource(ModInfo.modGUID);

            harmony.PatchAll(typeof(CruiserDamagePatchBase));
            harmony.PatchAll(typeof(CruiserDamagePatch));
            harmony.PatchAll(typeof(NetworkPatch));

            logSource.LogInfo(ModInfo.modName + " (version - " + ModInfo.modVersion + ")" + ": patches applied successfully");
        }
    }
}
