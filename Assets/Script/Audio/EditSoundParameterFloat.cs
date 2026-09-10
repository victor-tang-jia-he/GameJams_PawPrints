using UnityEngine;

public class EditSoundParameterFloat : MonoBehaviour
{
    [SerializeField] private string parameterName = "";
    [SerializeField] private float parameterValue = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollectableObject.PLAYER_TAG))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
        }
    }
}
