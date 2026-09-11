using Com.DefaultCompany.LostSoul;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Collider))]
public class TriggerCinematics : MonoBehaviour
{
    public VideoClip videoClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollectableObject.PLAYER_TAG))
        {
            if (videoClip == null)
            {
                Debug.LogWarning("Video clip is not assigned in TriggerCinematics.");
                return;
            }
            CoreSystem.Instance.GetService<UIManager>().PlayVideo(videoClip);
        }
    }
}
