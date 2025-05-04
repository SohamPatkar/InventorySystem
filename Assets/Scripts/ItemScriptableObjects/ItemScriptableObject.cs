using UnityEngine;

[CreateAssetMenu(menuName = "ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    public string nameOfItem;
    public string id;
    public string description;
    public Sprite icon;
    public int sellingPrice;
    public int costPrice;
    public ItemRarity itemRarity;
    public ItemType itemType;
    public ItemInventoryType itemInventoryType;
    public int quantity;
    public int weight;
}
