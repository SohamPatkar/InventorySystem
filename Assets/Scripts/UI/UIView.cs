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
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private Button addButton, subtractButton, buyButton;
    [SerializeField] private TextMeshProUGUI nameText, descpText, quantityText, priceText;
    [SerializeField] private Image itemIcon;
    private ItemModel tempItemModel;

    private void OnEnable()
    {
        EventService.Instance.OpenBuyPanel.AddListener(OpenPanel);
        EventService.Instance.ShowItemsUI.AddListener(SetItemsShop);
        EventService.Instance.ShowItemsUI.AddListener(SetItemsPlayer);
    }

    public void Initialize()
    {
        playerController = GameService.Instance.GetPlayerController();
        shopController = GameService.Instance.GetShopController();
        playerInventory = playerController.GetPlayerInventory();
        shopInventory = shopController.GetShopInventory();
    }

    public void OpenPanel(ItemModel item)
    {
        buyPanel.SetActive(true);
        BuyPanel(item);
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
        playerController.AddItems(tempItemModel);
        buyPanel.SetActive(false);
    }

    public void SetItemsShop()
    {
        int i = 0;

        foreach (ItemModel item in shopController.GetList())
        {
            ItemView newItem = shopInventory.transform.GetChild(i).gameObject.GetComponent<ItemView>();

            newItem.SetImage(item);
            newItem.SetQuantity(item);
            i++;
        }
    }

    public void SetItemsPlayer()
    {
        int i = 0;

        foreach (ItemModel item in playerController.GetItemsList())
        {
            ItemView newItem = playerInventory.transform.GetChild(i).gameObject.GetComponent<ItemView>();

            newItem.SetImage(item);
            newItem.SetQuantity(item);
            i++;
        }
    }
}
