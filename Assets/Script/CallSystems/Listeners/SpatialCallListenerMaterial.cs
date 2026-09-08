using TMPro;
using UnityEngine;
using UnityEngine.Splines.Interpolators;


public class SpatialCallListenerMaterial : SpatialCallListener
{

    
    [SerializeField]
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        DisableMaterial();
    }

    public override void ListenToCall(CallData callData)
    {
        if (!spriteRenderer.enabled)
        {
            spriteRenderer.enabled = true;
            Invoke(nameof(DisableMaterial), 10.0f);
        }
    }

    private void DisableMaterial()
    {
        spriteRenderer.enabled = false;
    }
}
