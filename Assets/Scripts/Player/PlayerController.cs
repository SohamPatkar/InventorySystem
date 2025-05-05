using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    public GameObject inventoryPanel;
    private ItemModel itemFound;

    public PlayerController(PlayerView playerView, GameObject inventoryPanel)
    {
        playerModel = new PlayerModel();
        this.playerView = GameObject.Instantiate(playerView, inventoryPanel.transform).GetComponent<PlayerView>();

        this.playerView.SetPlayerController(this);
    }

    public int GetCarryWeight()
    {
        return playerModel.carryWeight;
    }

    public int GetMaxCarryWeight()
    {
        return playerModel.maxCarryWeight;
    }

    public ItemModel GetItemFound()
    {
        return itemFound;
    }

    public bool HasItem(ItemModel item)
    {
        foreach (ItemModel findItem in playerModel.items)
        {
            if (item.id == findItem.id)
            {
                itemFound = findItem;
                return true;
            }
        }

        return false;
    }

    public int GetPlayerCoins()
    {
        return playerModel.coins;
    }

    public GameObject GetPlayerInventory()
    {
        return playerView.gameObject.transform.GetChild(1).gameObject;
    }

    public void AddItems(ItemModel item)
    {
        if (item.itemInventoryType == ItemInventoryType.NONE)
        {
            playerModel.coins += item.sellingPrice;
        }
        else if (item.itemInventoryType == ItemInventoryType.SHOPINVENTORY)
        {
            playerModel.coins -= item.costPrice;
        }


        if (playerModel.carryWeight >= playerModel.maxCarryWeight)
        {
            EventService.Instance.ShowItemsUI.InvokeEvent();
            return;
        }

        if (HasItem(item))
        {
            itemFound.quantity += 1;
            playerModel.carryWeight += item.weight;
            EventService.Instance.ShowItemsUI.InvokeEvent();
            return;
        }

        ItemModel newItem = new ItemModel(item.itemSo);

        playerModel.items.Add(newItem);
        playerModel.carryWeight += item.weight;
        item.itemInventoryType = ItemInventoryType.PLAYERINVENTORY;
        EventService.Instance.ShowItemsUI.InvokeEvent();
    }

    public void RemoveItems(ItemModel item)
    {
        playerModel.items.Remove(item);
    }

    public List<ItemModel> GetItemsList()
    {
        return playerModel.items;
    }
}
