using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    private GameObject inventoryPanel;

    public PlayerController(PlayerView playerView, GameObject inventoryPanel)
    {
        playerModel = new PlayerModel();
        this.playerView = GameObject.Instantiate(playerView, inventoryPanel.transform).GetComponent<PlayerView>();

        this.playerView.SetPlayerController(this);
    }

    public void AddItems(ItemModel item)
    {
        playerModel.items.Add(item);
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
