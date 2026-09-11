using Com.DefaultCompany.LostSoul;
using UnityEngine;
using UnityEngine.Video;

public class InteractableCollider : MonoBehaviour,IInteractable
{
    [SerializeField]
    private Collider colliderToDisable;

    public VideoClip videoClip;
    void IInteractable.Interact()
    {
        if (videoClip != null)
        {
            CoreSystem.Instance.GetService<UIManager>().PlayVideo(videoClip);
            colliderToDisable.enabled = false;
            Destroy(gameObject);
        }
    }
}
