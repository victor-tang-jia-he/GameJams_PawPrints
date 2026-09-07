using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItemInfo", menuName = "Scriptable Objects/Collectable Item Info")]
public class CollectableItemInfo : ScriptableObject
{
    [Header("Default Setting")]
    public int id;
    public string description;
    public Sprite smallSprite;
    public Sprite backgroundSprite;


    // Bonus
    [Header("Bonus setting for later")]
    public Sprite icon;

}
