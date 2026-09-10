using Com.DefaultCompany.LostSoul;
using UnityEngine;
using UnityEngine.UI;

public class Settings : Manager<Settings>
{
    public SO_Settings settings;

    [Header("Settings UI Setting")]
    public Slider masterVolume;
    public Slider sfxVolume;
    public Slider musicVolume;

    [Header("Settings Fmod bus path")]
    public string masterBusPath = "bus:/";
    public string sfxBusPath = "bus:/SFX";
    public string musicBusPath = "bus:/MUSIC";

    FMOD.Studio.Bus MasterBus;
    FMOD.Studio.Bus MusicBus;
    FMOD.Studio.Bus SFXBus;

    private void Awake()
    {
        if (settings == null || masterVolume == null || sfxVolume == null || musicVolume == null)
        {
            Debug.LogError("Settings can't work properly without those object or setting assigned, check if anything is missing before starting the game");
            return;
        }

        MasterBus = FMODUnity.RuntimeManager.GetBus(masterBusPath);
        MusicBus = FMODUnity.RuntimeManager.GetBus(musicBusPath);
        SFXBus = FMODUnity.RuntimeManager.GetBus(sfxBusPath);

        Load();
    }

    public void Save()
    {
        settings.MasterVolume = masterVolume.value;
        settings.SFXVolume = sfxVolume.value;
        settings.MusicVolume = musicVolume.value;

        MasterBus.setVolume(settings.MasterVolume);
        SFXBus.setVolume(settings.SFXVolume);
        MusicBus.setVolume(settings.MusicVolume);

        PlayerPrefs.SetFloat("MasterVolume", settings.MasterVolume);
        PlayerPrefs.SetFloat("SFXVolume", settings.SFXVolume);
        PlayerPrefs.SetFloat("MusicVolume", settings.MusicVolume);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        settings.MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        settings.SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        settings.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        MasterBus.setVolume(settings.MasterVolume);
        SFXBus.setVolume(settings.SFXVolume);
        MusicBus.setVolume(settings.MusicVolume);

        masterVolume.value = settings.MasterVolume;
        sfxVolume.value = settings.SFXVolume;
        musicVolume.value = settings.MusicVolume;
    }
}
