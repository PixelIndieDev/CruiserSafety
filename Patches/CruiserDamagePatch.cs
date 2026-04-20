using HarmonyLib;
using UnityEngine;

namespace CruiserSafety.Patches
{
    [HarmonyPatch(typeof(VehicleController))]
    internal class CruiserDamagePatch
    {
        [HarmonyPatch("DamagePlayerInVehicle")]
        [HarmonyPrefix]
        [HarmonyPriority(Priority.Last)]
        static bool PatchVehicle(VehicleController __instance, Vector3 vel, float magnitude)
        {
            if (__instance.localPlayerInPassengerSeat || __instance.localPlayerInControl)
            {
                if (magnitude > 24f)
                {
                    HUDManager.Instance.ShakeCamera(ScreenShakeType.VeryStrong);
                }
                else
                {
                    HUDManager.Instance.ShakeCamera(ScreenShakeType.Big);
                }
            }
            else if (__instance.physicsRegion.physicsTransform == GameNetworkManager.Instance.localPlayerController.physicsParent && GameNetworkManager.Instance.localPlayerController.overridePhysicsParent == null)
            {
                HUDManager.Instance.ShakeCamera(ScreenShakeType.Small);
                GameNetworkManager.Instance.localPlayerController.externalForceAutoFade += vel;
            }

            return false;
        }
    }
}
