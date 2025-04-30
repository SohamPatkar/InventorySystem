using UnityEngine;

public class ItemController : IInteractable
{
    public ItemView itemView;
    public ItemModel itemModel;
    public GameObject panel;

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
        itemModel.quantity += 1;
        itemView.itemSprite = itemModel.icon;
        itemView.SetItemController(this);
    }

    public void SetItemQuantity()
    {
        itemModel.quantity += 1;
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
        switch (itemModel.itemInventoryType)
        {
            case ItemInventoryType.SHOPINVENTORY:
                EventService.Instance.AddPlayerItems.InvokeEvent(this);
                EventService.Instance.UpdateUICoins.InvokeEvent();
                SetItemInventory(ItemInventoryType.PLAYERINVENTORY);
                GameService.Instance.GetShopController().RemoveItem(this);
                GameService.Instance.GetPlayerController().AddItems(this);
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
