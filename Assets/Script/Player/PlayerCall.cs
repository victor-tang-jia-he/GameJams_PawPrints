using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCall : MonoBehaviour
{
    public float CallRadius;

    [SerializeField]
    private float callCooldown = 1.0f;

    private bool isOnCooldown = false;
    private Timer cooldownTimer;
    void Awake()
    {
        
    }

    void Update()
    {
        if (isOnCooldown)
        {
            isOnCooldown = !cooldownTimer.AddTime(Time.deltaTime);
        }
    }

    public void Call(InputAction.CallbackContext callbackContext)
    {
        
        if (isOnCooldown)
        {
            return;
        }
        Debug.Log("Player Call");
        isOnCooldown = true;
        cooldownTimer = new Timer(callCooldown);

        GlobalCallInvoker.Singleton.GlobalCall(transform.position, CallRadius, 3.0f);
        
    }
}
