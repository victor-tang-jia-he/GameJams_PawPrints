using System.Collections.Generic;
using UnityEngine;

public class BarkZones : MonoBehaviour
{
    public FMODUnity.EventReference soundToEmit;
    public List<SpatialCallListenerSoundEmitter> barkZones = new();

    private void Awake()
    {
        if (barkZones.Count == 0)
        {
            return;
        }
        for (int i =0;i < barkZones.Count - 1;i++)
        {
            barkZones[i].soundToEmit = soundToEmit;
            barkZones[i].soundSourceObject = barkZones[i + 1].gameObject;
        }
    }
}
