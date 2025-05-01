using UnityEngine;

public class ItemController : IInteractable
{
    public ItemView itemView;
    public ItemModel itemModel;
    public GameObject panel;
    private int playerCoins;

    public ItemController(ItemScriptableObject itemScriptableObject, ItemView itemView, GameObject panel)
    {
        itemModel = new ItemModel(itemScriptableObject);
        this.panel = panel;


        CreateItem(itemView);
    }

    public void CreateItem(ItemView item)
    {
        GameObject itemObject = GameObject.Instantiate(item.gameObject, panel.transform);
        itemView = itemObject.GetComponent<ItemView>();
        SetItemQuantity();
        itemView.itemSprite = itemModel.icon;
        itemView.itemQuantity.text = GetItemQuantity().ToString();
        itemView.SetItemController(this);
    }

    public void SetItemQuantity()
    {
        itemModel.quantity += 1;
    }

    public string GetItemDescription()
    {
        return itemModel.description;
    }

    public string GetItemName()
    {
        return itemModel.Name;
    }

    public Sprite GetItemSprite()
    {
        return itemModel.icon;
    }

    public int GetItemQuantity()
    {
        return itemModel.quantity;
    }

    public string GetItemId()
    {
        return itemModel.id;
    }

    public int GetItemSellingPrice()
    {
        return itemModel.sellingPrice;
    }

    public int GetItemCostPrice()
    {
        return itemModel.costPrice;
    }

    public void SetItemInventory(ItemInventoryType itemInventoryType)
    {
        itemModel.itemInventoryType = itemInventoryType;
    }

    public void OnInteract()
    {
        //Get player coins and check if they have the amount
        playerCoins = GameService.Instance.GetPlayerController().GetPlayerCoins();

        if (playerCoins <= 0 || playerCoins < itemModel.costPrice)
        {
            return;
        }

        switch (itemModel.itemInventoryType)
        {
            case ItemInventoryType.SHOPINVENTORY:
                EventService.Instance.BuyItem.InvokeEvent(this);
                break;

            case ItemInventoryType.PLAYERINVENTORY:
                EventService.Instance.UpdateUICoins.InvokeEvent();
                SetItemInventory(ItemInventoryType.SHOPINVENTORY);
                GameService.Instance.GetShopController().AddItem(this);
                GameService.Instance.GetPlayerController().RemoveItems(this);
                break;
        }
    }
}
