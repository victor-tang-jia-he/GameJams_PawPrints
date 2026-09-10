using Com.DefaultCompany.LostSoul;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    private bool hasBeenShown = false;
    private bool hasBeenHidden = false;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        if (!hasBeenShown || hasBeenHidden)
            return;

        if (CoreSystem.Instance
            .GetService<PlayerInputHandler>()
            .GetSubmitPressed())
        {
            Hide();
            hasBeenHidden = true;
        }
    }

    public void Show()
    {
        if (hasBeenShown)
            return;

        popup.SetActive(true);
        hasBeenShown = true;
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