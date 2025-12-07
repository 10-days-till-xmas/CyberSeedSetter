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
        return SceneHelper.CurrentScene != "Endless"
                   ? Branch("cybergrind-seed",
                       Leaf<int>("set", Setter),
                       Leaf("get", Getter))
                   : Branch("cybergrind-seed",
                       Leaf<int>("set", Setter));
    }
    private static void Setter(int seed) => Plugin.Configs.rngSeed.Value = seed;
    private void Getter() => Log.Info($"Current seed value: {RandomManager.RandomInstance.Seed}");
}