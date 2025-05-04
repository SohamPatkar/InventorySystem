using System.Collections.Generic;
using UnityEngine;

public class ShopController
{
    public ShopModel shopModel;
    public ShopView shopView;
    public ItemModel itemFound;
    public ShopController(ShopView shopView, Transform inventoryPanel)
    {
        shopModel = new ShopModel();
        this.shopView = GameObject.Instantiate(shopView.gameObject, inventoryPanel).GetComponent<ShopView>();

        this.shopView.SetShopController(this);
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
        return shopView.shopInventoryPanel;
    }

    public void AddItem(ItemModel item)
    {
        if (HasItem(item))
        {
            itemFound.quantity += 1;

            EventService.Instance.ShowItemsUI.InvokeEvent();
            return;
        }

        shopModel.items.Add(item);
        EventService.Instance.ShowItemsUI.InvokeEvent();
    }

    public void RemoveItem(ItemModel item)
    {
        shopModel.items.Add(item);
    }

}
