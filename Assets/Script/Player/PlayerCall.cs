using Com.DefaultCompany.LostSoul;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCall : MonoBehaviour
{
    public float CallRadius;

    [SerializeField]
    private float callCooldown = 1.0f;

    [SerializeField]
    private FMODUnity.EventReference playerCallSound;

    
    private bool canCall = true;
    private bool isOnCooldown = false;
    private Timer cooldownTimer;

    private GlobalCallInvoker callInvoker;
    private void Start()
    {
        callInvoker = CoreSystem.Instance.GetService<GlobalCallInvoker>();
        CoreSystem.Instance.GetService<PlayerInputHandler>().OnPlayerCallInput.AddListener(Call);

        DialogueManager dialogueManager = CoreSystem.Instance.GetService<DialogueManager>();
        dialogueManager.OnDialogueStarted.AddListener(DisableCall);
        dialogueManager.OnDialogueEnded.AddListener(EnableCall);
        
    }
    void Update()
    {
        if (isOnCooldown)
        {
            isOnCooldown = !cooldownTimer.AddTime(Time.deltaTime);
        }
    }

    public void SetCallEnable(bool enable)
    {
        canCall = enable;
    }

    public void DisableCall()
    {
        SetCallEnable(false);
    }

    public void EnableCall()
    {
        SetCallEnable(true);
    }
    public void Call()
    {
        
        if (isOnCooldown || !canCall)
        {
            return;
        }
        Debug.Log("Player Call");
        isOnCooldown = true;
        cooldownTimer = new Timer(callCooldown);
        FMODUnity.RuntimeManager.PlayOneShot(playerCallSound);
        callInvoker.GlobalCall(transform.position, CallRadius, 1.0f);
        
    }
    private void OnDestroy()
    {
        DialogueManager dialogueManager = CoreSystem.Instance.GetService<DialogueManager>();
        dialogueManager.OnDialogueStarted.RemoveListener(DisableCall);
        dialogueManager.OnDialogueEnded.RemoveListener(EnableCall);
    }
}
