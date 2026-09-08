using UnityEngine;

[CreateAssetMenu(fileName = "InteractableItemInfo", menuName = "Scriptable Objects/Interactable Item Info")]
public class InteractableItemInfo : ScriptableObject
{
    [Header("Default Setting")]
    public int id;
    public string knotName;
    public string description;
    public Mesh mesh;
    public Sprite backgroundSprite;


    // Bonus
    [Header("Bonus setting for later")]
    public Sprite icon;

}
