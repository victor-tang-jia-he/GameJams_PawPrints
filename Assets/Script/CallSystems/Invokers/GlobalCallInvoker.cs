using System;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalCallInvoker : MonoBehaviour
{
    public GlobalCallInvoker Singleton;

    [SerializeField]
    private GameObject basicCallObject;
    void Awake()
    {
        if (Singleton != null)
        {
            Singleton = this;
        }
    }
    public SpatialCall GlobalCall(Vector3 position)
    {
        GameObject newObject = Instantiate(basicCallObject, position,Quaternion.identity);
        SpatialCall spatialCall = newObject.AddComponent<SpatialCall>();
        spatialCall.MakeCall();

        return spatialCall;
    }

    public SpatialCall GlobalCall(Vector3 position, float lifeTime)
    {
        SpatialCall spatialCall = GlobalCall(position);
        Destroy(spatialCall.gameObject, lifeTime);
        return spatialCall;
    }
}
