using System.Collections.Generic;
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

    public List<ItemModel> GetItemsList()
    {
        return playerModel.items;
    }

    public bool CarryWeightExceeded(ItemModel item)
    {
        return playerModel.carryWeight + item.weight > playerModel.maxCarryWeight;
    }

    public void AddItems(ItemModel item)
    {
        if (CarryWeightExceeded(item))
        {
            EventService.Instance.ShowErrorText.InvokeEvent();
            return;
        }

        if (HasItem(item))
        {
            itemFound.quantity += 1;
            playerModel.carryWeight += item.weight;

            EventService.Instance.ShowItemsUI.InvokeEvent();
            return;
        }

        if (item.itemInventoryType == ItemInventoryType.NONE)
        {
            playerModel.items.Add(item);
            playerModel.carryWeight += item.weight;
            item.itemInventoryType = ItemInventoryType.PLAYERINVENTORY;
            EventService.Instance.ShowItemsUI.InvokeEvent();
        }
        else
        {
            ItemModel newItem = new ItemModel(item.itemSo, ItemInventoryType.PLAYERINVENTORY);
            playerModel.items.Add(newItem);
            playerModel.carryWeight += item.weight;
            newItem.itemInventoryType = ItemInventoryType.PLAYERINVENTORY;
            EventService.Instance.ShowItemsUI.InvokeEvent();
        }
    }

    public void SetCoins(ItemModel item)
    {
        if (item.itemInventoryType == ItemInventoryType.NONE || item.itemInventoryType == ItemInventoryType.PLAYERINVENTORY)
        {
            playerModel.coins += item.sellingPrice;
        }
        else if (item.itemInventoryType == ItemInventoryType.SHOPINVENTORY)
        {
            playerModel.coins -= item.costPrice;

            if (playerModel.coins < 0)
            {
                playerModel.coins = 0;
            }
        }
    }

    public void RemoveItems(ItemModel item)
    {
        if (HasItem(item))
        {
            if (itemFound.quantity > 1)
            {
                playerModel.carryWeight -= item.weight;
                itemFound.quantity -= 1;
                SetCoins(item);
                EventService.Instance.ShowItemsUI.InvokeEvent();
                return;
            }

            playerModel.carryWeight -= item.weight;
            playerModel.items.Remove(itemFound);
            SetCoins(item);
            EventService.Instance.ShowItemsUI.InvokeEvent();
            return;
        }
    }


}
