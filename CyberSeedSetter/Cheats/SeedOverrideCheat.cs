using System.Text;
using UnityEngine;

namespace CyberSeedSetter.Cheats;

public sealed class SeedOverrideCheat : ICheat
{
    public string LongName => "Cybergrind Seed Override";
    public string Identifier => "cyberseedsetter.seed-override";
    public string ButtonEnabledOverride => null!;
    public string ButtonDisabledOverride => null!;
    public string Icon => "noclip";
    public bool DefaultState => false;
    public StatePersistenceMode PersistenceMode => StatePersistenceMode.Persistent;
    public int Seed { get; private set; } = Random.Range(int.MinValue, int.MaxValue);
    public static int CurrentSeed => CheatsManager.Instance.GetCheatInstance<SeedOverrideCheat>().Seed;
    public static System.Random RandomInstance { get; private set; } = new();
    public bool IsActive { get; private set; }
    public void Enable(CheatsManager manager)
    {
        IsActive = true;
        Seed = Plugin.Configs.rngSeed.Value;
        RandomInstance = new System.Random(Seed);
        SubtitleController.Instance.DisplaySubtitle("Random state disrupted: restart to use the set seed properly.", ignoreSetting: false);
    }

    public void Disable()
    {
        IsActive = false;
        Seed = Random.Range(int.MinValue, int.MaxValue);
        RandomInstance = new System.Random(Seed);
        SubtitleController.Instance.DisplaySubtitle("Random state disrupted: restart to continue using a set seed.", ignoreSetting: false);
    }

}