using Com.DefaultCompany.LostSoul;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCall : MonoBehaviour
{
    public float CallRadius;

    [SerializeField]
    private float callCooldown = 1.0f;

    private bool isOnCooldown = false;
    private Timer cooldownTimer;

    private GlobalCallInvoker callInvoker;
    private void Start()
    {
        callInvoker = CoreSystem.Instance.GetService<GlobalCallInvoker>();
        CoreSystem.Instance.GetService<PlayerInputHandler>().OnPlayerCallInput.AddListener(Call);
    }
    void Update()
    {
        if (isOnCooldown)
        {
            isOnCooldown = !cooldownTimer.AddTime(Time.deltaTime);
        }
    }

    public void Call()
    {
        
        if (isOnCooldown)
        {
            return;
        }
        Debug.Log("Player Call");
        isOnCooldown = true;
        cooldownTimer = new Timer(callCooldown);

       callInvoker.GlobalCall(transform.position, CallRadius, 1.0f);
        
    }
}
