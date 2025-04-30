using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coins;
    private PlayerController playerController;

    private void Start()
    {
        EventService.Instance.AddPlayerItems.AddListener(UpdatePlayerItems);
        EventService.Instance.AddPlayerItems.AddListener(UpdateShopItems);
        EventService.Instance.UpdateUICoins.AddListener(SetCoins);
    }

    public void InitializeVariables()
    {
        playerController = GameService.Instance.GetPlayerController();
        SetCoins();
    }

    public void SetCoins()
    {
        coins.text = "Coins: " + GameService.Instance.GetPlayerController().GetPlayerCoins();
    }

    public void UpdatePlayerItems(ItemController item)
    {
        if (playerController.HasItem(item))
        {
            ItemController itemFound = playerController.GetItemFound();
            itemFound.SetItemQuantity();
            itemFound.itemView.itemQuantity.text = itemFound.GetItemQuantity().ToString();
            item.itemView.gameObject.SetActive(false);
        }
        else
        {
            item.itemView.gameObject.transform.SetParent(playerController.GetPlayerInventory().transform);
        }
    }

    public void UpdateShopItems(ItemController item)
    {

    }

    public void GenerateItemsPlayer()
    {
        GameService.Instance.CreatePlayerItems(playerController.GetPlayerInventory());
    }

}
