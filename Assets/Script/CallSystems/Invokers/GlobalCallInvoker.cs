using Com.DefaultCompany.LostSoul;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalCallInvoker : Manager<GlobalCallInvoker>
{
    
    
    public SpatialCall GlobalCall(Vector3 position,float callRadius)
    {
        GameObject newObject = new GameObject();
        SpatialCall spatialCall = newObject.AddComponent<SpatialCall>();
        newObject.transform.position = position;
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
