using UnityEngine;

public class ItemController : IInteractable
{
    public ItemView itemView;
    public ItemModel itemModel;
    public GameObject shopView, playerInventory;

    public ItemController(ItemScriptableObject itemScriptableObject, ItemView itemView, GameObject shopView)
    {
        itemModel = new ItemModel(itemScriptableObject);
        this.shopView = shopView;


        CreateItem(itemView);
    }

    public void CreateItem(ItemView item)
    {
        GameObject itemObject = GameObject.Instantiate(item.gameObject, shopView.transform);
        itemView = itemObject.GetComponent<ItemView>();
        itemModel.quantity += 1;
        itemView.itemSprite = itemModel.icon;
        itemView.SetItemController(this);
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
                GameService.Instance.GetPlayerController().AddItems(this);
                SetItemInventory(ItemInventoryType.PLAYERINVENTORY);
                EventService.Instance.AddPlayerItems.InvokeEvent(this);
                break;

            case ItemInventoryType.PLAYERINVENTORY:
                GameService.Instance.GetShopController().AddItem(this);
                SetItemInventory(ItemInventoryType.SHOPINVENTORY);
                break;
        }
    }
}
