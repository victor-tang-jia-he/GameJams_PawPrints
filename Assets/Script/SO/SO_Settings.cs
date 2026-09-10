using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class SO_Settings : ScriptableObject
{
    [Range(0, 1)] public float MasterVolume = 1f;
    [Range(0, 1)] public float SFXVolume = 1f;
    [Range(0, 1)] public float MusicVolume = 1f;
}
