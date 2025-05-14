using System.Collections.Generic;
using UnityEngine;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;
    private ItemModel itemFound;
    private List<Transform> slotList;
    public ShopController(ShopView shopView, Transform inventoryPanel)
    {
        shopModel = new ShopModel();
        this.shopView = GameObject.Instantiate(shopView.gameObject, inventoryPanel).GetComponent<ShopView>();

        this.shopView.SetShopController(this);
        PopulateSlots();
    }

    public List<ItemModel> GetList()
    {
        return shopModel.items;
    }

    public bool HasItem(ItemModel item)
    {
        foreach (ItemModel findItem in shopModel.items)
        {
            if (item.id == findItem.id)
            {
                itemFound = findItem;
                return true;
            }
        }

        return false;
    }

    public GameObject GetShopInventory()
    {
        return shopView.ReturnShopInventoryPanel();
    }

    private void PopulateSlots()
    {
        slotList = shopView.GetSlots();
    }

    public void AddItem(ItemModel item)
    {
        if (HasItem(item))
        {
            itemFound.quantity += 1;
            SetItemsShop();
            return;
        }

        if (item.itemInventoryType == ItemInventoryType.PLAYERINVENTORY)
        {
            ItemModel newItem = new ItemModel(item.itemSo, ItemInventoryType.SHOPINVENTORY);
            shopModel.items.Add(newItem);
            SetItemsShop();
            return;
        }

        shopModel.items.Add(item);
        SetItemsShop();
    }

    public void RemoveItem(ItemModel item)
    {
        if (HasItem(item))
        {
            if (itemFound.quantity > 1)
            {
                itemFound.quantity -= 1;
                SetItemsShop();
                return;
            }

            shopModel.items.Remove(itemFound);
            SetItemsShop();
            return;
        }
    }

    public void Tabfunctions(ItemType itemType)
    {
        var shopItems = GetList();

        int totalSlots = GetShopInventory().transform.childCount;

        for (int i = 0; i < totalSlots; i++)
        {
            Transform slot = GetShopInventory().transform.GetChild(i);
            ItemView newItem = slot.GetComponent<ItemView>();

            if (i < shopItems.Count)
            {
                ItemModel item = shopItems[i];

                if (item.itemType == itemType)
                {
                    slot.gameObject.SetActive(true);
                    newItem.SetImage(item);
                    newItem.SetQuantity(item);
                }
                else
                {
                    slot.gameObject.SetActive(false);
                }
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
        }
    }

    public void SetItemsShop()
    {
        var shopItems = GetList();

        int totalSlots = GetShopInventory().transform.childCount;

        for (int i = 0; i < totalSlots; i++)
        {
            Transform slot = slotList[i];
            ItemView newItem = slot.GetComponent<ItemView>();

            if (i < shopItems.Count)
            {
                ItemModel item = shopItems[i];

                slot.gameObject.SetActive(true);
                newItem.SetImage(item);
                newItem.SetQuantity(item);
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
        }
    }

}
