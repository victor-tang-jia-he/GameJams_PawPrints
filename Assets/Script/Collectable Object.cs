using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CollectableObject : MonoBehaviour
{
    public CollectableItemInfo item;
    public List<string> dialogueText;
    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
        if (item  == null)
        {
            Debug.LogWarning($"Collectable Object Item info missing");
            return;
        }

        item = GetComponent<CollectableItemInfo>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = item.smallSprite;
    }
}
