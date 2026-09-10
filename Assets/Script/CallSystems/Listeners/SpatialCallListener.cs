using UnityEngine;
using UnityEngine.Events;

public struct CallData
{
    public SpatialCall spatialCall;
    public GameObject callOwner;
}
public class SpatialCallListener : MonoBehaviour
{
    public UnityEvent<CallData> OnListenToCallEvent = new();
    public virtual void ListenToCall(CallData callData)
    {
        OnListenToCallEvent.Invoke(callData);
        Debug.Log("Listening");
    }
}
