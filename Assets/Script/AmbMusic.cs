using Com.DefaultCompany.LostSoul;
using FMOD.Studio;
using System;
using UnityEngine;

public class AmbMusic : MonoBehaviour
{
    public FMODUnity.EventReference ambMusicReference;
    FMOD.Studio.EventInstance ambMusicInstance;

    private UIManager uiManager;

    private void Start()
    {
        ambMusicInstance = FMODUnity.RuntimeManager.CreateInstance(ambMusicReference);
        ambMusicInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        ambMusicInstance.start();
        uiManager = CoreSystem.Instance.GetService<UIManager>();

        if (uiManager != null)
        {
            uiManager.OnSceneChange += StopAmbMusic;
        }
        else
        {
            Debug.LogError("One or more services not found in CoreSystem.");
        }
    }
    private void OnDestroy()
    {
        if (uiManager != null)
        {
            uiManager.OnSceneChange -= StopAmbMusic;
        }
    }

    private void StopAmbMusic()
    {
        ambMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambMusicInstance.release();
    }
}