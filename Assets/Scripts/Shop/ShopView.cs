using UnityEngine;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;
    [SerializeField] public GameObject shopInventoryPanel;

    void Start()
    {
        foreach (ItemModel item in shopController.GetList())
        {
            Debug.Log(item.id);
        }
    }

    public void SetShopController(ShopController shopController)
    {
        this.shopController = shopController;
    }
}
