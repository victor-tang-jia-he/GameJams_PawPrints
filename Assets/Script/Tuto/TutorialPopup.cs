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
