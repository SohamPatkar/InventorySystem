using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [Header("Player")]
    private PlayerController playerController;
    private GameObject playerInventory;
    [SerializeField] private TextMeshProUGUI coins;

    [Header("Shop")]
    private ShopController shopController;
    private GameObject shopInventory;

    [Header("UI")]
    [SerializeField] private GameObject buyPanel, popupText, confirmationPanel;
    [SerializeField] private TextMeshProUGUI nameText, descpText, quantityText, priceText, carryWeight, btnText, itemInventType, confirmationText;
    [SerializeField] private Image itemIcon;
    private ItemModel tempItemModel;
    private string errorMessage, popupMessage;
    private int itemQuantity = 1;
    private bool isInitialized = false;

    private void Awake()
    {
        if (!isInitialized)
        {
            EventService.Instance.OpenBuyPanel.AddListener(OpenPanel);
            EventService.Instance.ShowItemsUI.AddListener(SetItemsShop);
            EventService.Instance.ShowItemsUI.AddListener(SetItemsPlayer);
            EventService.Instance.ShowItemsUI.AddListener(SetCarryWeight);
            EventService.Instance.ShowItemsUI.AddListener(SetCoinsText);
            EventService.Instance.ShowErrorText.AddListener(ErrorText);
            EventService.Instance.TabPressed.AddListener(Tabfunctions);

            isInitialized = true;
        }
    }

    public void Initialize()
    {
        playerController = GameService.Instance.GetPlayerController();
        shopController = GameService.Instance.GetShopController();
        playerInventory = playerController.GetPlayerInventory();
        shopInventory = shopController.GetShopInventory();
    }

    public void AddQuantity()
    {
        if (itemQuantity < tempItemModel.quantity)
        {
            itemQuantity += 1;
        }

        quantityText.text = "" + itemQuantity;
    }

    public void SubtractQuantity()
    {
        if (itemQuantity > 1)
        {
            itemQuantity -= 1;
        }

        quantityText.text = "" + itemQuantity;
    }

    public void SetCoinsText()
    {
        coins.text = "Player Gold: " + playerController.GetPlayerCoins();
    }

    public void OpenPanel(ItemModel item)
    {
        buyPanel.SetActive(true);
        BuyPanel(item);
    }

    public void AddItemsPlayer()
    {
        GameService.Instance.CreatePlayerItems(playerInventory.gameObject);
    }

    public void DeactivatePanels()
    {
        if (confirmationPanel)
        {
            confirmationPanel.SetActive(false);
        }
        buyPanel.SetActive(false);
    }

    public void SetCarryWeight()
    {
        carryWeight.text = "Carry Weight: " + playerController.GetCarryWeight();
    }

    public void ErrorText()
    {
        errorMessage = "You are full my friend";
        SetPopupText(errorMessage);

        StartCoroutine(DisableErrorText());
    }

    public void SetPopupText(string message)
    {
        popupText.SetActive(true);
        popupText.GetComponent<TextMeshProUGUI>().color = Color.red;
        popupText.GetComponent<TextMeshProUGUI>().text = message;
    }

    public IEnumerator DisableErrorText()
    {
        yield return new WaitForSeconds(3f);
        popupText.SetActive(false);
    }

    public void ConfirmationPanel()
    {
        confirmationPanel.SetActive(true);

        if (tempItemModel.itemInventoryType == ItemInventoryType.PLAYERINVENTORY)
        {
            confirmationText.text = "Do you want to sell " + itemQuantity + " " + tempItemModel.nameOfItem + " ?";
        }
        else if (tempItemModel.itemInventoryType == ItemInventoryType.SHOPINVENTORY)
        {
            confirmationText.text = "Do you want to buy " + itemQuantity + " " + tempItemModel.nameOfItem + " ?";
        }
        else
        {
            return;
        }
    }


    public void BuyPanel(ItemModel item)
    {
        //setting the item details and resetting the quantity
        ResetItemQuantity();

        tempItemModel = item;

        itemIcon.sprite = item.icon;
        nameText.text = "Name: " + item.nameOfItem;
        descpText.text = "Description: " + item.description;
        itemInventType.text = "Belongs to: " + item.itemInventoryType.ToString();

        if (item.itemInventoryType == ItemInventoryType.SHOPINVENTORY)
        {
            priceText.text = "Price: " + item.costPrice;
            btnText.text = "Buy";
        }
        else if (item.itemInventoryType == ItemInventoryType.PLAYERINVENTORY)
        {
            priceText.text = "Price: " + item.sellingPrice;
            btnText.text = "Sell";
        }
        else
        {
            Debug.Log("Kill me please I don't code nicely");
        }
    }

    public void ResetItemQuantity()
    {
        itemQuantity = 1;
        quantityText.text = "" + itemQuantity;
    }

    public void Transaction()
    {
        if (tempItemModel.itemInventoryType == ItemInventoryType.SHOPINVENTORY)
        {
            BuyItem();
        }
        else if (tempItemModel.itemInventoryType == ItemInventoryType.PLAYERINVENTORY)
        {
            SellItem();
        }
        else
        {
            errorMessage = "Not Enough Coins";
            SetPopupText(errorMessage);
            StartCoroutine(DisableErrorText());
            DeactivatePanels();
        }
    }

    private void BuyItem()
    {
        int totalCost = tempItemModel.costPrice * itemQuantity;

        if (playerController.GetPlayerCoins() < totalCost)
        {
            errorMessage = "Not Enough Coins";
            SetPopupText(errorMessage);
            StartCoroutine(DisableErrorText());
            DeactivatePanels();
            return;
        }

        if (playerController.GetCarryWeight() + (itemQuantity * tempItemModel.weight) > playerController.GetMaxCarryWeight())
        {
            EventService.Instance.ShowErrorText.InvokeEvent();
            return;
        }

        for (int i = 0; i < itemQuantity; i++)
        {
            shopController.RemoveItem(tempItemModel);
            playerController.SetCoins(tempItemModel);
            playerController.AddItems(tempItemModel);
        }

        popupMessage = "Item bought";
        SetPopupText(popupMessage);

        GameService.Instance.GetSoundManager().PlaySfx(SoundType.BOUGHTSOUND);

        ResetItemQuantity();
        DeactivatePanels();
        StartCoroutine(DisableErrorText());
    }

    private void SellItem()
    {
        for (int i = 0; i < itemQuantity; i++)
        {
            playerController.RemoveItems(tempItemModel);
            shopController.AddItem(tempItemModel);

            Debug.Log($"Sell clicked with quantity: {itemQuantity}");
        }

        popupMessage = "Item sold";
        SetPopupText(popupMessage);

        GameService.Instance.GetSoundManager().PlaySfx(SoundType.SOLDSOUND);

        ResetItemQuantity();
        DeactivatePanels();
        StartCoroutine(DisableErrorText());
    }

    public void Tabfunctions(ItemType itemType)
    {
        var shopItems = shopController.GetList();

        int totalSlots = shopInventory.transform.childCount;

        for (int i = 0; i < totalSlots; i++)
        {
            Transform slot = shopInventory.transform.GetChild(i);
            ItemView newItem = slot.GetComponent<ItemView>();

            if (i < shopItems.Count)
            {
                ItemModel item = shopItems[i];

                if (item.itemType == itemType)
                {
                    slot.gameObject.SetActive(true);
                    newItem.SetImage(item);
                    newItem.SetQuantity(item);
                }
                else
                {
                    slot.gameObject.SetActive(false);
                }
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
        }
    }

    public void SetItemsShop()
    {
        var shopItems = shopController.GetList();

        int totalSlots = shopInventory.transform.childCount;

        for (int i = 0; i < totalSlots; i++)
        {
            Transform slot = shopInventory.transform.GetChild(i);
            ItemView newItem = slot.GetComponent<ItemView>();

            if (i < shopItems.Count)
            {
                ItemModel item = shopItems[i];

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

    public void SetItemsPlayer()
    {
        var playerItems = playerController.GetItemsList();

        int totalSlots = playerInventory.transform.childCount;

        for (int i = 0; i < totalSlots; i++)
        {
            Transform slot = playerInventory.transform.GetChild(i);
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

    private void OnDisable()
    {
        EventService.Instance.OpenBuyPanel.RemoveListener(OpenPanel);
        EventService.Instance.ShowItemsUI.RemoveListener(SetItemsShop);
        EventService.Instance.ShowItemsUI.RemoveListener(SetItemsPlayer);
        EventService.Instance.ShowItemsUI.RemoveListener(SetCarryWeight);
        EventService.Instance.ShowErrorText.RemoveListener(ErrorText);
        EventService.Instance.TabPressed.RemoveListener(Tabfunctions);
    }
}

