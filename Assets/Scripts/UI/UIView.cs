using TMPro;
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
    [SerializeField] private GameObject buyPanel, errorText;
    [SerializeField] private Button addButton, subtractButton, buyButton;
    [SerializeField] private TextMeshProUGUI nameText, descpText, quantityText, priceText, carryWeight;
    [SerializeField] private Image itemIcon;
    private ItemModel tempItemModel;
    private string errorMessage;

    private void OnEnable()
    {
        EventService.Instance.OpenBuyPanel.AddListener(OpenPanel);
        EventService.Instance.ShowItemsUI.AddListener(SetItemsShop);
        EventService.Instance.ShowItemsUI.AddListener(SetItemsPlayer);
        EventService.Instance.ShowItemsUI.AddListener(SetCarryWeight);
        EventService.Instance.ShowItemsUI.AddListener(ErrorText);
        EventService.Instance.ShowItemsUI.AddListener(SetCoinsText);
    }

    public void Initialize()
    {
        playerController = GameService.Instance.GetPlayerController();
        shopController = GameService.Instance.GetShopController();
        playerInventory = playerController.GetPlayerInventory();
        shopInventory = shopController.GetShopInventory();
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

    public void BuyPanel(ItemModel item)
    {
        //setting the details
        tempItemModel = item;

        itemIcon.sprite = item.icon;
        nameText.text = "Name: " + item.nameOfITem;
        descpText.text = "Description: " + item.description;

        if (item.itemInventoryType == ItemInventoryType.SHOPINVENTORY)
        {
            priceText.text = "Price: " + item.costPrice;
        }
        else if (item.itemInventoryType == ItemInventoryType.PLAYERINVENTORY)
        {
            priceText.text = "Price: " + item.sellingPrice;
        }
    }

    public void OnClickBuy()
    {
        if (playerController.GetPlayerCoins() >= tempItemModel.costPrice)
        {
            shopController.RemoveItem(tempItemModel);
            playerController.AddItems(tempItemModel);
        }

        errorMessage = "Not Enough Coins";
        errorText.SetActive(true);
        errorText.GetComponent<TextMeshProUGUI>().text = errorMessage;
        Invoke("DisableErrorText", 3f);

        buyPanel.SetActive(false);
    }

    public void SetCarryWeight()
    {
        carryWeight.text = "Carry Weight: " + playerController.GetCarryWeight();
    }

    public void ErrorText()
    {
        if (playerController.GetCarryWeight() >= playerController.GetMaxCarryWeight())
        {
            errorMessage = "You are full my friend";
            errorText.SetActive(true);
            errorText.GetComponent<TextMeshProUGUI>().color = Color.red;
            errorText.GetComponent<TextMeshProUGUI>().text = errorMessage;
        }


        Invoke("DisableErrorText", 3f);
    }

    public void DisableErrorText()
    {
        errorText.SetActive(false);
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
    }

}

