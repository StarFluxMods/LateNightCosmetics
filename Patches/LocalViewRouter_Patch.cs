using System.Reflection;
using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using KitchenLib.Utils;
using UnityEngine;

namespace LateNightCosmetics.Patches
{
    [HarmonyPatch(typeof(LocalViewRouter), "GetPrefab")]
    public class LocalViewRouter_Patch
    {
        static void Prefix(LocalViewRouter __instance, ViewType view_type, ref GameObject __result)
        {
            if (Mod.GridMenuConfig != null) return;
            FieldInfo _AssetDirectory = ReflectionUtils.GetField<LocalViewRouter>("AssetDirectory");
            AssetDirectory assetDirectory = (AssetDirectory)_AssetDirectory.GetValue(__instance);
            if (assetDirectory.ViewPrefabs.TryGetValue(ViewType.CostumeChangeInfo, out GameObject costumeChangeInfo))
            {
                CostumeChangeIndicator costumeChangeIndicator = costumeChangeInfo.GetComponent<CostumeChangeIndicator>();
                FieldInfo RootMenuConfig = ReflectionUtils.GetField<CostumeChangeIndicator>("RootMenuConfig");
                Mod.GridMenuConfig = (GridMenuNavigationConfig)RootMenuConfig.GetValue(costumeChangeIndicator);
                Mod.GridMenuConfig.Icon = Mod.GridMenuConfig.Links[1].Icon;
                
                if (Mod.Register != null)
                {
                    Mod.Register.Invoke(null, new object[]{ Mod.GridMenuConfig });
                }
            }
        }
    }
}