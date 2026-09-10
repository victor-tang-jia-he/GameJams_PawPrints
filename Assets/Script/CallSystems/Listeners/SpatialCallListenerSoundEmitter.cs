using UnityEngine;

public class SpatialCallListenerSoundEmitter : SpatialCallListener
{
    
    public FMODUnity.EventReference soundToEmit;

    
    public GameObject soundSourceObject;

    private bool IsPlayerInZone = false;
    public override void ListenToCall(CallData callData)
    {
        if (IsPlayerInZone && soundSourceObject != null)
        {
            base.ListenToCall(callData);
            FMODUnity.RuntimeManager.PlayOneShot(soundToEmit, soundSourceObject.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerInZone = true;
            Debug.Log("Player in zone");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerInZone = false;
            Debug.Log("Player out zone");
        }
    }
}
