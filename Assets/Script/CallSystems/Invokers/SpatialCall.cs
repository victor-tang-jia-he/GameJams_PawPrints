using System.Threading;
using UnityEngine;

public class SpatialCall : MonoBehaviour
{
    public float CallRadius;

    public virtual void MakeCall(GameObject owner)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CallRadius);

        foreach(Collider collider in hitColliders)
        {
            SpatialCallListener callListener = collider.GetComponent<SpatialCallListener>();
            if (callListener != null)
            {
                CallData callData;
                callData.callOwner = owner;
                callData.spatialCall = this;
                callListener.ListenToCall(callData);
            }
        }
    }
    private void OnDrawGizmos()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CallRadius);
    }
}
