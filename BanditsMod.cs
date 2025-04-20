// Project: BanditsMod (stable-22.4 7 Days to Die, server-side)
// Folder: Mods/BanditsMod/
// References: Assembly-CSharp.dll, UnityEngine.CoreModule.dll, 0Harmony.dll

using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace BanditsMod
{
    // -------------------------------------------------------------------
    // 1. Entry Point - BanditsMod.cs
    // -------------------------------------------------------------------
    [HarmonyPatch]
    public static class BanditsMod
    {
        static BanditsMod()
        {
            // Apply Harmony patches
            var harmony = new Harmony("com.yourname.banditsmod");
            harmony.PatchAll();
            Debug.Log("[BanditsMod] Harmony patches applied");

            // Register server-side bandit spawner
            BanditSpawner.Initialize();
        }
    }
}