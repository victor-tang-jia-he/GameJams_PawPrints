using TMPro;
using UnityEngine;
using UnityEngine.Splines.Interpolators;


public class SpatialCallListenerMaterial : SpatialCallListener
{

    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    
    private StateMachine glowStateMachine;
    private Material material;

    [SerializeField]
    private float glowingTime;
    [SerializeField]
    private float glowedTime;
    [SerializeField]
    private float unglowingTime;

    private void Awake()
    {
        glowStateMachine = GlowStateMachineInit.Init(glowingTime, glowedTime, unglowingTime);
        glowStateMachine.Initialize<UnglowState>();
        OnUnglow();

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
