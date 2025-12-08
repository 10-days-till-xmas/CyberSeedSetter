using System;
using System.Globalization;
using GameConsole;
using GameConsole.CommandTree;
using UnityEngine;
using Console = GameConsole.Console;
using Logger = plog.Logger;

namespace CyberSeedSetter.ConsoleCommands;

public sealed class SeedCommands(Console con) : CommandRoot(con), IConsoleLogger
{
    public override string Name => "Set Seed Command";
    public override string Description => "Commands for setting/getting the RNG seed for Cybergrind wave creation.";
    public Logger Log => new(Name);

    protected override Branch BuildTree(Console con)
    {
        return SceneHelper.CurrentScene != "Endless"
                   ? Branch("cybergrind-seed", [
                           ..SetterNodes,
                           ..GetterNodes])
                   : Branch("cybergrind-seed",
                       SetterNodes);
    }

    private Node[] SetterNodes =>
    [
        Leaf<int>("set", Set),
        Leaf<string>("set-str", SetStr),
        Leaf("paste", Paste)
    ];
    private Node[] GetterNodes =>
    [
        Leaf("get", Get),
        Leaf("copy", Copy)
    ];

    private static void Set(int seed) => Plugin.Configs.rngSeed.Value = seed;
    private void SetStr(string seed) =>
        Set(ParseSeed(seed,
            onParsed: (s, v) => Log.Info($"Parsed seed value: \"{s}\" -> {v}"),
            onFailed: (s, v) => Log.Info($"Hashed seed value: \"{s}\" -> {v}")));

    private void Get() => Log.Info($"Current seed value: {RandomManager.RandomInstance.Seed}");
    private void Copy()
    {
        Log.Info($"Copied seed value to clipboard: ({RandomManager.RandomInstance.Seed})");
        GUIUtility.systemCopyBuffer = RandomManager.RandomInstance.Seed.ToString(CultureInfo.CurrentCulture);
    }

    private void Paste() => SetStr(GUIUtility.systemCopyBuffer);

    private static int ParseSeed(string seed, Action<string, int>? onParsed = null, Action<string, int>? onFailed = null)
    {
        if (int.TryParse(seed, NumberStyles.Number, CultureInfo.CurrentCulture, out var res))
            onParsed?.Invoke(seed, res);
        else
        {
            res = seed.GetHashCode();
            onFailed?.Invoke(seed, res);
        }
        return res;
    }
}