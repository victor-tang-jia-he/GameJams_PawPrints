using System.Threading;
using UnityEngine;

public class SpatialCall : MonoBehaviour
{
    public float CallRadius;

    public virtual void MakeCall()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CallRadius);

        foreach(Collider collider in hitColliders)
        {
            SpatialCallListener callListener = collider.GetComponent<SpatialCallListener>();
            if (callListener != null)
            {
                CallData callData;
                callData.caller = gameObject;
                callListener.ListenToCall(callData);
            }
        }
    }
}
