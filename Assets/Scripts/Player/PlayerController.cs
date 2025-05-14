using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    private ItemModel itemFound;

    public PlayerController(PlayerView playerView, GameObject inventoryPanel)
    {
        playerModel = new PlayerModel();
        this.playerView = GameObject.Instantiate(playerView, inventoryPanel.transform).GetComponent<PlayerView>();

        this.playerView.SetPlayerController(this);
    }

    public int GetPlayerInventoryValue()
    {
        int valueOfPlayerInventory = 0;

        List<ItemModel> playerInventoryList = GetItemsList();

        foreach (ItemModel item in playerInventoryList)
        {
            valueOfPlayerInventory += item.sellingPrice * item.quantity;
        }

        return valueOfPlayerInventory;
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

    public void AddItems(ItemModel item)
    {
        if (playerModel.carryWeight + item.weight > playerModel.maxCarryWeight)
        {
            EventService.Instance.OnExceedWeight.InvokeEvent();
            return;
        }

        if (HasItem(item))
        {
            itemFound.quantity += 1;
            playerModel.carryWeight += item.weight;
        }
        else
        {
            ItemModel newItem = new ItemModel(item.itemSo, ItemInventoryType.PLAYERINVENTORY);
            playerModel.items.Add(newItem);
            playerModel.carryWeight += item.weight;
        }

        SetItemsPlayer();
        EventService.Instance.ShowItemsShop.InvokeEvent();
        EventService.Instance.UpdateWeight.InvokeEvent(GetCarryWeight());
    }

    public void BuyItem(ItemModel item, int quantity)
    {
        if (playerModel.coins < item.costPrice * quantity)
        {
            EventService.Instance.NotEnoughCoinsText.InvokeEvent();
            return;
        }

        if (playerModel.carryWeight + (item.weight * quantity) >= playerModel.maxCarryWeight)
        {
            EventService.Instance.OnExceedWeight.InvokeEvent();
            return;
        }

        for (int i = 0; i < quantity; i++)
        {
            playerModel.coins -= item.costPrice;
            if (playerModel.coins < 0)
            {
                playerModel.coins = 0;
            }

            GameService.Instance.GetShopController().RemoveItem(item);
            AddItems(item);
        }

        GameService.Instance.GetSoundManager().PlaySfx(SoundType.BOUGHTSOUND);
        EventService.Instance.ItemBoughtText.InvokeEvent();
        EventService.Instance.UpdateCoins.InvokeEvent(GetPlayerCoins());
    }

    public void SellItem(ItemModel item, int quantity)
    {
        if (!HasItem(item))
        {
            return;
        }

        for (int i = 0; i < quantity; i++)
        {
            playerModel.coins += item.sellingPrice;
            GameService.Instance.GetShopController().AddItem(item);
            RemoveItems(item);
        }

        GameService.Instance.GetSoundManager().PlaySfx(SoundType.SOLDSOUND);
        EventService.Instance.ItemSoldText.InvokeEvent();
        EventService.Instance.UpdateCoins.InvokeEvent(GetPlayerCoins());
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

                SetItemsPlayer();
                EventService.Instance.ShowItemsShop.InvokeEvent();
                EventService.Instance.UpdateWeight.InvokeEvent(GetCarryWeight());
                return;
            }

            playerModel.carryWeight -= item.weight;
            playerModel.items.Remove(itemFound);

            SetItemsPlayer();
            EventService.Instance.ShowItemsShop.InvokeEvent();
            EventService.Instance.UpdateWeight.InvokeEvent(GetCarryWeight());
            return;
        }
    }

    public void SetItemsPlayer()
    {
        var playerItems = GetItemsList();

        int totalSlots = GetPlayerInventory().transform.childCount;

        for (int i = 0; i < totalSlots; i++)
        {
            Transform slot = GetPlayerInventory().transform.GetChild(i);
            ItemView newItem = slot.GetComponent<ItemView>();

            if (i < playerItems.Count)
            {
                ItemModel item = playerItems[i];

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
