using Com.DefaultCompany.LostSoul;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalCallInvoker : Manager<GlobalCallInvoker>
{
    [SerializeField]
    private GameObject defaultSpatialCall;
    public SpatialCall GlobalCall(GameObject callOwner,Vector3 position,float callRadius)
    {
        GameObject newObject = Instantiate(defaultSpatialCall, position, Quaternion.identity);
        SpatialCall spatialCall = newObject.GetComponent<SpatialCall>();
        spatialCall.CallRadius = callRadius;
        spatialCall.MakeCall(callOwner); 

        return spatialCall;
    }

    public SpatialCall GlobalCall(GameObject callOwner, Vector3 position, float callRadius, float lifeTime)
    {
        SpatialCall spatialCall = GlobalCall(callOwner,position,callRadius);
        Destroy(spatialCall.gameObject, lifeTime);
        return spatialCall;
    }
}
