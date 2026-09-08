using UnityEngine;
using UnityEngine.Events;

public struct CallData
{
    public GameObject caller;
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
