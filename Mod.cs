using System;
using KitchenLib;
using KitchenMods;
using System.Reflection;
using Kitchen.Modules;
using KitchenLib.Interfaces;
using KitchenLib.Utils;
using KitchenLogger = KitchenLib.Logging.KitchenLogger;

namespace LateNightCosmetics
{
    public class Mod : BaseMod, IModSystem, IAutoRegisterAll
    {
        public const string MOD_GUID = "com.starfluxgames.latenightcosmetics";
        public const string MOD_NAME = "Late Night Cosmetics";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.2.0";

        internal static KitchenLogger Logger;
        internal static MethodInfo Register;
        internal static GridMenuNavigationConfig GridMenuConfig;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnUpdate()
        {
        }
        
        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Logger = InitLogger();
            CheckForOptionalMod();
        }

        private void CheckForOptionalMod()
        {
            foreach (KitchenMods.Mod loadedMod in ModPreload.Mods)
            {
                foreach (AssemblyModPack pack in loadedMod.GetPacks<AssemblyModPack>())
                {
                    foreach (Type type in pack.Asm.GetTypes())
                    {
                        if (type.FullName != "FrontDoorAppliances.Utils.FrontDoorMenus") continue;
                        
                        Register = ReflectionUtils.GetMethod(type, "Register");
                        return;
                    }
                }
            }
        }
    }
}