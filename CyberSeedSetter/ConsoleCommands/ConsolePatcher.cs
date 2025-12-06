using GameConsole;
using HarmonyLib;

namespace CyberSeedSetter.ConsoleCommands;
[HarmonyPatch(typeof(Console))]
public class ConsolePatcher
{
    [HarmonyPostfix]
    [HarmonyPatch("Awake")]
    private static void AwakePostfix(Console __instance)
    {
        Plugin.Logger.LogInfo("Registering SeedCommands");
        __instance.RegisterCommand(new SeedCommands(__instance));
    }
}