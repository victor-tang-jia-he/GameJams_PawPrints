using System;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalCallInvoker : MonoBehaviour
{
    public static GlobalCallInvoker Singleton;

    
    private GameObject basicCallObject;
    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        basicCallObject = new GameObject();
        basicCallObject.AddComponent<SpatialCall>();
        basicCallObject.SetActive(false);
    }
    public SpatialCall GlobalCall(Vector3 position,float callRadius)
    {
        GameObject newObject = Instantiate(basicCallObject, position,Quaternion.identity);
        newObject.SetActive(true);
        SpatialCall spatialCall = newObject.AddComponent<SpatialCall>();
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
