using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    public GameObject inventoryPanel;
    private ItemController item;

    public PlayerController(PlayerView playerView, GameObject inventoryPanel)
    {
        playerModel = new PlayerModel();
        this.playerView = GameObject.Instantiate(playerView, inventoryPanel.transform).GetComponent<PlayerView>();

        this.playerView.SetPlayerController(this);
    }

    public ItemController GetItemFound()
    {
        return item;
    }

    public int GetPlayerCoins()
    {
        return playerModel.coins;
    }

    public GameObject GetPlayerInventory()
    {
        return playerView.gameObject.transform.GetChild(1).gameObject;
    }

    public void AddItems(ItemController item)
    {
        playerModel.items.Add(item);
    }

    public void SetPlayerCoins(ItemController item)
    {
        if (item.itemModel.itemInventoryType == ItemInventoryType.SHOPINVENTORY)
        {
            playerModel.coins -= item.GetItemCostPrice();
        }
        else
        {
            playerModel.coins += item.GetItemSellingPrice();
        }
    }

    public bool HasItem(ItemController item)
    {
        foreach (ItemController item2 in playerModel.items)
        {
            if (item2.GetItemId() == item.GetItemId())
            {
                this.item = item2;
                return true;
            }
        }

        return false;
    }

    public void RemoveItems(ItemController item)
    {
        playerModel.items.Remove(item);
    }

    public List<ItemController> GetItemsList()
    {
        return playerModel.items;
    }
}
