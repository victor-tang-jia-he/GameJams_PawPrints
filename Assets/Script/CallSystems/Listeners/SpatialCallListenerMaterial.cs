using TMPro;
using UnityEngine;
using UnityEngine.Splines.Interpolators;


public class SpatialCallListenerMaterial : SpatialCallListener
{

    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float visibleTime;
    private void Awake()
    {
        DisableMaterial();
    }

    public override void ListenToCall(CallData callData)
    {
        if (!spriteRenderer.enabled)
        {
            spriteRenderer.enabled = true;
            Invoke(nameof(DisableMaterial), visibleTime);
        }
    }

    private void DisableMaterial()
    {
        spriteRenderer.enabled = false;
    }
}
