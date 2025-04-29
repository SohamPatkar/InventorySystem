using UnityEngine;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;
    [SerializeField] private ItemView itemView;
    [SerializeField] private GameObject shopInventory;
    [SerializeField] private ItemScriptableObject[] itemScriptableObjects;

    void Start()
    {
        AddItems();
    }

    public void AddItems()
    {
        foreach (var item in itemScriptableObjects)
        {
            ItemController newItem = new ItemController(item, itemView, shopInventory);

            shopController.AddItem(newItem);
        }
    }

    public void SetShopController(ShopController shopController)
    {
        this.shopController = shopController;
    }
}
