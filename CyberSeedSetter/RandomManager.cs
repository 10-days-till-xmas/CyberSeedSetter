using System;

namespace CyberSeedSetter;

public static class RandomManager
{
    public static SeededRandom RandomInstance { get; private set; } = null!;

    public static void Initialize(bool seeded)
    {
        RandomInstance = seeded
                             ? new SeededRandom(Plugin.Configs.rngSeed.Value)
                             : new SeededRandom();
        Plugin.Logger.LogInfo($"Initializing RandomManager with seed = {RandomInstance.Seed}");
        Plugin.FileLogger
              .GetFile(FileLogger.LogKeys.History)
              .AppendLines($"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}: {RandomInstance.Seed}");
    }
}