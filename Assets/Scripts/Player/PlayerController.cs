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
        if (HasItem(item))
        {
            itemFound.quantity += 1;

            EventService.Instance.ShowItemsUI.InvokeEvent();
            return;
        }

        playerModel.items.Add(item);
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
