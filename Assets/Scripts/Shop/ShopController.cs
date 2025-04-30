using UnityEngine;

public class ShopController
{
    public ShopModel shopModel;
    public ShopView shopView;
    public ShopController(ShopView shopView, Transform inventoryPanel)
    {
        shopModel = new ShopModel();
        this.shopView = GameObject.Instantiate(shopView.gameObject, inventoryPanel).GetComponent<ShopView>();

        this.shopView.SetShopController(this);
    }

    public GameObject GetShopInventory()
    {
        return shopView.shopInventoryPanel;
    }

    public void AddItem(ItemController item)
    {
        shopModel.items.Add(item);
    }
}
