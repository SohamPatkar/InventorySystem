using UnityEngine;

public class ItemModel
{
    public string nameOfITem;
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

    public ItemModel(ItemScriptableObject itemScriptableObject)
    {
        itemSo = itemScriptableObject;

        InitializeValues(itemScriptableObject);
    }

    public void InitializeValues(ItemScriptableObject item)
    {
        nameOfITem = item.nameOfItem;
        id = item.id;
        description = item.description;
        icon = item.icon;
        sellingPrice = item.sellingPrice;
        costPrice = item.costPrice;
        itemRarity = item.itemRarity;
        itemType = item.itemType;
        itemInventoryType = item.itemInventoryType;
        quantity = item.quantity;
        weight = item.weight;
    }
}
