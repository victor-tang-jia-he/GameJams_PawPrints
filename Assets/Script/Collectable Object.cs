using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CollectableObject : MonoBehaviour
{
    public CollectableItemInfo item;
    public string dialogueKnotName;
    public MeshFilter meshFilter;

    private DialogueManager dialogueManager;
    private PlayerInputHandler playerInputHandler;

    static public string PLAYER_TAG = string.Empty; // TODO : Add it to the player
    public void Awake()
    {
        if (item == null)
        {
            Debug.LogWarning($"Collectable Object Item info missing");
            return;
        }

        meshFilter = GetComponent<MeshFilter>();

        meshFilter.mesh = item.mesh;
    }

    private void Start()
    {
        dialogueManager = DialogueManager.Instance;
    }
    private void Update()
    {
        if ((playerInputHandler = PlayerInputHandler.Instance) == null) return; // Lazy getter
        if (playerInputHandler.GetInteractPressed())
        {
            Interact();
        }
    }
    // Called by the player
    public void Interact()
    {
        if (dialogueManager != null)
        {
            StartCoroutine(dialogueManager.CO_EnterDialogue(dialogueKnotName));
        }
    }
}
