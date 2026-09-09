using Com.DefaultCompany.LostSoul;
using System;
using UnityEngine;

public class AmbMusic : MonoBehaviour
{
    public FMODUnity.EventReference ambMusicReference;
    FMOD.Studio.EventInstance ambMusicInstance;

    private UIManager uiManager;

    private void Awake()
    {
        ambMusicInstance = FMODUnity.RuntimeManager.CreateInstance(ambMusicReference);
        ambMusicInstance.start();
    }

    private void Start()
    {
        uiManager = CoreSystem.Instance.GetService<UIManager>();
        if (uiManager != null)
        {
            uiManager.OnSceneChange += StopAmbMusic;
        }
        else
        {
            Debug.LogError("UIManager not found in CoreSystem.");
        }
    }
    private void OnDestroy()
    {
        CoreSystem.Instance.GetService<UIManager>().OnSceneChange -= StopAmbMusic;
    }

    private void StopAmbMusic()
    {
        Debug.Log("Stopping ambient music");
        ambMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambMusicInstance.release();
    }
}