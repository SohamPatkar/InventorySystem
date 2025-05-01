using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [Header("Player")]
    private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI coins;

    [Header("UI View")]
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject buyPanel;

    [Header("Item")]
    [SerializeField] private Image buyPanelItemSprite;
    [SerializeField] private TextMeshProUGUI itemNameText, itemDescriptionText, itemPriceText;
    private ItemController itemToChange;

    private void Start()
    {
        EventService.Instance.AddPlayerItems.AddListener(UpdatePlayerItems);
        EventService.Instance.BuyItem.AddListener(ShowBuyPanel);
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

    public void ShowBuyPanel(ItemController item)
    {
        buyPanel.SetActive(true);
        itemToChange = item;
        buyPanelItemSprite.sprite = item.GetItemSprite();
        itemNameText.text = "Name: " + item.GetItemName();
        itemDescriptionText.text = "Description: " + item.GetItemDescription();
        itemPriceText.text = "Item Price: " + item.GetItemCostPrice();
    }

    public void BuyButton()
    {
        EventService.Instance.AddPlayerItems.InvokeEvent(itemToChange);
        EventService.Instance.UpdateUICoins.InvokeEvent();
        itemToChange.SetItemInventory(ItemInventoryType.PLAYERINVENTORY);
        GameService.Instance.GetShopController().RemoveItem(itemToChange);
        GameService.Instance.GetPlayerController().AddItems(itemToChange);
        buyPanel.SetActive(false);
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
        //To be implemented
    }

    public void GenerateItemsPlayer()
    {
        GameService.Instance.CreatePlayerItems(playerController.GetPlayerInventory());
    }

}
