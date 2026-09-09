using TMPro;
using UnityEngine;
using UnityEngine.Splines.Interpolators;


public class SpatialCallListenerMaterial : SpatialCallListener
{

    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float visibleTime;

    private StateMachine glowStateMachine;
    private Material material;

    private void Awake()
    {
        glowStateMachine = GlowStateMachineInit.Init(2, 5, 2);
        glowStateMachine.Initialize<UnglowState>();

        GlowingState glowingState = glowStateMachine.GetState<GlowingState>();
        FullyGlowedState fullyGlowedState = glowStateMachine.GetState<FullyGlowedState>();
        UnglowingState unglowingState = glowStateMachine.GetState<UnglowingState>();

        material = spriteRenderer.material;

        glowingState.OnFullyGlowedEvent.AddListener(OnGlow);
        glowingState.GlowingEvent.AddListener(OnGlowing);

        unglowingState.UnglowingEvent.AddListener(OnUnglowing);
        unglowingState.OnUnglowedEvent.AddListener(OnUnglow);
    }

    private void Update()
    {
        glowStateMachine.Update();
    }
    public override void ListenToCall(CallData callData)
    {
        if (!spriteRenderer.enabled)
        {
            glowStateMachine.TransitionTo<GlowingState>();
        }
    }

    private void OnGlowing(float ratio)
    {
        spriteRenderer.enabled = true;
        Color currentMaterialColor = material.color;
        currentMaterialColor.a = ratio;
        material.color = currentMaterialColor;
    }
    private void OnGlow()
    {
        spriteRenderer.enabled = true;
    }
    

    private void OnUnglowing(float ratio)
    {
        Color currentMaterialColor = material.color;
        currentMaterialColor.a = 1 - ratio;
        material.color = currentMaterialColor;
    }

    private void OnUnglow()
    {
        spriteRenderer.enabled = false;
    }
    
}
