using System;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace CyberSeedSetter;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public sealed class Plugin : BaseUnityPlugin
{
    // Note: the mod doesn't work yet for some reason. Possible causes is the random object not actually being initialized with the seed properly,
    //       or the transpiler not working as intended.
    public sealed class PluginConfigs(ConfigFile config)
    {
        public readonly ConfigEntry<int> rngSeed = config.Bind(
        section: "general",
        key: "RNG Seed",
        defaultValue: 0,
        description: "sets the seed for the random number generator, leave null for a random seed"
        );
    }
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static PluginConfigs Configs { get; private set; } = null!;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        gameObject.hideFlags = HideFlags.DontSaveInEditor;
        Configs = new PluginConfigs(Config);
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
        var m_EndlessGridPatcher_Random = ((Delegate)EndlessGridPatcher.Random_Range_Replacer).Method;
        AccessTools.GetDeclaredMethods(typeof(EndlessGrid))
                   .Where(static m => m.HasMethodBody()
                                   && m.MemberType == MemberTypes.Method
                                   && !m.IsSpecialName)
                   .Do(method => harmony.Patch(method, transpiler: new HarmonyMethod(m_EndlessGridPatcher_Random)));
    }
}