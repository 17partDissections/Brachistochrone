using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Name & Icon")]
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemIcon;
    [Header("Prefab")]
    public GameObject ItemPrefab;
    [Header("Sounds")]
    public AudioClip ItemSound_Pickup;
    public AudioClip ItemSound_Drop;
    public AudioClip ItemSound_Use;
}
