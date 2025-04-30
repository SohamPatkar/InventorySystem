using UnityEngine;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;
    [SerializeField] public GameObject shopInventoryPanel;

    void Start()
    {

    }

    // public void AddItems()
    // {
    //     foreach (var item in itemScriptableObjects)
    //     {
    //         ItemController newItem = new ItemController(item, itemView, shopInventory);

    //         shopController.AddItem(newItem);
    //     }
    // }

    public void SetShopController(ShopController shopController)
    {
        this.shopController = shopController;
    }
}
