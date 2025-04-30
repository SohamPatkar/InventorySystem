using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    public GameObject inventoryPanel;

    public PlayerController(PlayerView playerView, GameObject inventoryPanel)
    {
        playerModel = new PlayerModel();
        this.playerView = GameObject.Instantiate(playerView, inventoryPanel.transform).GetComponent<PlayerView>();

        this.playerView.SetPlayerController(this);
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
            playerModel.coins += item.GetItemCostPrice();
        }
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
