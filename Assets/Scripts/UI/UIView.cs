using System.Collections;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coins;

    [Header("UI")]
    [SerializeField] private GameObject transactionPanel;
    [SerializeField] private GameObject popupText;
    [SerializeField] private GameObject confirmationPanel;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descpText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI carryWeight;
    [SerializeField] private TextMeshProUGUI btnText;
    [SerializeField] private TextMeshProUGUI itemInventType;
    [SerializeField] private TextMeshProUGUI confirmationText;
    [SerializeField] private Image itemIcon;
    private ItemModel tempItemModel;
    private string errorMessage, popupMessage;
    private int itemQuantity = 1;
    private bool isInitialized = false;

    private void Awake()
    {
        if (!isInitialized)
        {
            EventService.Instance.OpenBuyPanel.AddListener(OpenTransactionPanel);
            EventService.Instance.UpdateWeight.AddListener(SetCarryWeight);
            EventService.Instance.UpdateCoins.AddListener(SetCoinsText);
            EventService.Instance.OnExceedWeight.AddListener(WeightFullText);
            EventService.Instance.NotEnoughCoinsText.AddListener(NotEnoughCoinsText);
            EventService.Instance.ItemBoughtText.AddListener(ItemBoughtText);
            EventService.Instance.ItemSoldText.AddListener(ItemSoldText);

            isInitialized = true;
        }
    }

    //this is for ui text
    public void AddQuantity()
    {
        if (itemQuantity < tempItemModel.quantity)
        {
            itemQuantity += 1;
        }

        quantityText.text = "" + itemQuantity;
    }

    //this is for ui text
    public void SubtractQuantity()
    {
        if (itemQuantity > 1)
        {
            itemQuantity -= 1;
        }

        quantityText.text = "" + itemQuantity;
    }

    public void SetCoinsText(int playerGold)
    {
        coins.text = "Player Gold: " + playerGold;
    }

    public void OpenTransactionPanel(ItemModel item)
    {
        transactionPanel.SetActive(true);
        TransactionPanel(item);
    }

    public void AddItemsPlayer()
    {
        GameService.Instance.CreatePlayerItems(GameService.Instance.GetPlayerController().GetPlayerInventory().gameObject);
    }

    public void DeactivatePanels()
    {
        confirmationPanel.SetActive(false);
        transactionPanel.SetActive(false);
    }

    public void CancelTransaction()
    {
        confirmationPanel.SetActive(false);
    }

    public void CancelTransaction()
    {
        confirmationPanel.SetActive(false);
    }

    public void SetCarryWeight(int weight)
    {
        carryWeight.text = "Carry Weight: " + weight;
    }

    public void WeightFullText()
    {
        errorMessage = "You are full my friend";
        SetPopupText(errorMessage);

        StartCoroutine(DisableErrorText());
    }

    public void SetPopupText(string message)
    {
        popupText.SetActive(true);
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


    public void TransactionPanel(ItemModel item)
    {
        //setting the item details and resetting the quantity
        ResetUIItemQuantity();

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
    }

    public void ResetUIItemQuantity()
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
            StartCoroutine(DisableErrorText());
            DeactivatePanels();
        }
    }

    private void NotEnoughCoinsText()
    {
        errorMessage = "Not Enough Coins";
        SetPopupText(errorMessage);
    }

    private void ItemBoughtText()
    {
        popupMessage = "Item bought";
        SetPopupText(popupMessage);
    }

    private void ItemSoldText()
    {
        popupMessage = "Item sold";
        SetPopupText(popupMessage);
    }

    private void BuyItem()
    {
        GameService.Instance.GetPlayerController().BuyItem(tempItemModel, itemQuantity);

        ResetUIItemQuantity();
        DeactivatePanels();
        StartCoroutine(DisableErrorText());
    }

    private void SellItem()
    {
        GameService.Instance.GetPlayerController().SellItem(tempItemModel, itemQuantity);

        ResetUIItemQuantity();
        DeactivatePanels();
        StartCoroutine(DisableErrorText());
    }

    private void OnDisable()
    {
        EventService.Instance.OpenBuyPanel.RemoveListener(OpenTransactionPanel);
        EventService.Instance.UpdateWeight.RemoveListener(SetCarryWeight);
        EventService.Instance.UpdateCoins.RemoveListener(SetCoinsText);
        EventService.Instance.OnExceedWeight.RemoveListener(WeightFullText);
        EventService.Instance.NotEnoughCoinsText.RemoveListener(NotEnoughCoinsText);
        EventService.Instance.ItemBoughtText.RemoveListener(ItemBoughtText);
        EventService.Instance.ItemSoldText.RemoveListener(ItemSoldText);
    }
}

