using Com.DefaultCompany.LostSoul;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    private bool hasBeenShown = false;

    private void Awake()
    {
        Hide();
    }
    public void Show()
    {
        if (!hasBeenShown)
        {
            popup.SetActive(true);
            hasBeenShown = true;
        }
    }

    private void Update()
    {
        if (!   hasBeenShown) return;
        if (popup.activeSelf && CoreSystem.Instance.GetService<PlayerInputHandler>().GetSubmitPressed())
        {
            Hide();
        }
    }

    public void Hide()
    {
        popup.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollectableObject.PLAYER_TAG))
        {
            Show();
        }
    }
}
