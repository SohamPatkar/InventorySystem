using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIView
{
    private GameObject playerInventory, shopInventory;
    public UIView()
    {
        playerInventory = GameService.Instance.GetPlayerController().GetPlayerInventory();
        EventService.Instance.AddPlayerItems.AddListener(UpdatePlayerItems);
    }

    public void UpdatePlayerItems(ItemController item)
    {
        item.itemView.gameObject.transform.SetParent(playerInventory.transform);
    }

}
