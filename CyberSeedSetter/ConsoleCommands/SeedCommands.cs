using CyberSeedSetter.Cheats;
using GameConsole;
using GameConsole.CommandTree;
using plog;

namespace CyberSeedSetter.ConsoleCommands;

public sealed class SeedCommands(Console con) : CommandRoot(con), IConsoleLogger
{
    public override string Name => "Set Seed Command";
    public override string Description => "Commands for setting/getting the RNG seed for Cybergrind wave creation.";
    public Logger Log => new(Name);

    protected override Branch BuildTree(Console con)
    {
        return Branch("cybergrind-seed",
            Leaf("set", static (int seed) => Plugin.Configs.rngSeed.Value = seed),
            Leaf("get", () => Log.Info($"Current seed value: {SeedOverrideCheat.CurrentSeed}")));
    }
}