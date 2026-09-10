using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;

    public Movement movement;

    private void Update()
    {
        if (movement != null && animator != null)
        {
            if (movement.IsMoving)
            {
                animator.SetBool("Move", true);
                return;
            }
        }
        animator.SetBool("Move", false);
    }
}
