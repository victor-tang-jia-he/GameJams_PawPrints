using Com.DefaultCompany.LostSoul;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalCallInvoker : Manager<GlobalCallInvoker>
{
    [SerializeField]
    private GameObject defaultSpatialCall;
    public SpatialCall GlobalCall(Vector3 position,float callRadius)
    {
        GameObject newObject = Instantiate(defaultSpatialCall, position, Quaternion.identity);
        SpatialCall spatialCall = newObject.GetComponent<SpatialCall>();
        spatialCall.CallRadius = callRadius;
        spatialCall.MakeCall(); 

        return spatialCall;
    }

    public SpatialCall GlobalCall(Vector3 position, float callRadius, float lifeTime)
    {
        SpatialCall spatialCall = GlobalCall(position,callRadius);
        Destroy(spatialCall.gameObject, lifeTime);
        return spatialCall;
    }
}
