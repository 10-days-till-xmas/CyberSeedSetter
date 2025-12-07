namespace CyberSeedSetter.Cheats;

public sealed class SeedOverrideCheat : ICheat
{
    public string LongName => "Cybergrind Seed Override";
    public string Identifier => IDENTIFIER;
    public const string IDENTIFIER = "cyberseedsetter.seed-override";
    public string ButtonEnabledOverride => null!;
    public string ButtonDisabledOverride => null!;
    public string Icon => "warning";
    public bool DefaultState => false;
    public StatePersistenceMode PersistenceMode => StatePersistenceMode.Persistent;
    public bool IsActive { get; private set; }

    public void Enable(CheatsManager manager)
    {
        IsActive = true;
        PrefsManager.Instance.SetBool("cheat.ultrakill.keep-enabled", true);
        SubtitleController.Instance.DisplaySubtitle("Restart the level with keep cheats enabled to apply the seed", ignoreSetting: false);
    }

    public void Disable()
    {
        IsActive = false;
        SubtitleController.Instance.DisplaySubtitle("Restart the level to play without the seed", ignoreSetting: false);
    }
}