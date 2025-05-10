using UnityEngine;

public class ItemModel
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
    public ItemScriptableObject itemSo;

    public ItemModel(ItemScriptableObject itemScriptableObject, ItemInventoryType itemInventoryType)
    {
        itemSo = itemScriptableObject;

        InitializeValues(itemScriptableObject, itemInventoryType);
    }

    public void InitializeValues(ItemScriptableObject item, ItemInventoryType itemInventoryType)
    {
        this.nameOfItem = item.nameOfItem;
        id = item.id;
        description = item.description;
        icon = item.icon;
        sellingPrice = item.sellingPrice;
        costPrice = item.costPrice;
        itemRarity = item.itemRarity;
        itemType = item.itemType;
        this.itemInventoryType = itemInventoryType;
        quantity = item.quantity;
        weight = item.weight;
    }
}
