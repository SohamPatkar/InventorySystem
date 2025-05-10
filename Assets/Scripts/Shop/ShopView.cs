using UnityEngine;
using UnityEngine.UI;
public class ShopView : MonoBehaviour
{
    private ShopController shopController;
    [SerializeField] public GameObject shopInventoryPanel;
    [SerializeField] private Button weaponTab, consumableTab, allTab, materialTab, treasureTab;

    void Start()
    {

    }

    public void SetShopController(ShopController shopController)
    {
        this.shopController = shopController;
    }

    public void SetWeaponTab()
    {
        shopController.TabCall(ItemType.WEAPON);
    }

    public void SetAllTab()
    {
        EventService.Instance.ShowItemsUI.InvokeEvent();
    }

    public void SetMaterialTab()
    {
        shopController.TabCall(ItemType.MATERIALS);
    }

    public void SetConsumablesTab()
    {
        shopController.TabCall(ItemType.CONSUMABLE);
    }

    public void SetTreasureTab()
    {
        shopController.TabCall(ItemType.TREASURE);
    }
}
