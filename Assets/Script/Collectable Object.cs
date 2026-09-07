using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CollectableObject : MonoBehaviour
{
    public CollectableItemInfo item;
    public string dialogueKnotName;
    public MeshFilter meshFilter;

    public void Awake()
    {
        if (item  == null)
        {
            Debug.LogWarning($"Collectable Object Item info missing");
            return;
        }

        meshFilter = GetComponent<MeshFilter>();

        meshFilter.mesh = item.mesh;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.instance?.EnterDialogue(dialogueKnotName);
        }
    }
}
