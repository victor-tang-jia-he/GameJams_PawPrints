using UnityEngine;

public class EditSoundParameterString : MonoBehaviour
{
    [SerializeField] private string parameterName = "";
    [SerializeField] private string parameterValue = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollectableObject.PLAYER_TAG))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel(parameterName, parameterValue);
        }
    }
}
