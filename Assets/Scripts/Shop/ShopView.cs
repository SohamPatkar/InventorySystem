using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopView : MonoBehaviour
{
    private ShopController shopController;
    [SerializeField] private Button weaponTab;
    [SerializeField] private Button consumableTab;
    [SerializeField] private Button allTab;
    [SerializeField] private Button materialTab;
    [SerializeField] private Button treasureTab;
    [SerializeField] private List<Transform> slots = new List<Transform>();

    [SerializeField] private GameObject shopInventoryPanel;

    private void Start()
    {
        EventService.Instance.ShowItemsShop.AddListener(shopController.SetItemsShop);
    }

    public void SetShopController(ShopController shopController)
    {
        this.shopController = shopController;
    }

    public void SetWeaponTab()
    {
        shopController.Tabfunctions(ItemType.WEAPON);
    }

    public void SetAllTab()
    {
        EventService.Instance.ShowItemsShop.InvokeEvent();
    }

    public void SetMaterialTab()
    {
        shopController.Tabfunctions(ItemType.MATERIALS);
    }

    public void SetConsumablesTab()
    {
        shopController.Tabfunctions(ItemType.CONSUMABLE);
    }

    public void SetTreasureTab()
    {
        shopController.Tabfunctions(ItemType.TREASURE);
    }

    public GameObject ReturnShopInventoryPanel()
    {
        return shopInventoryPanel;
    }

    public List<Transform> GetSlots()
    {
        return slots;
    }

    private void OnDisable()
    {
        EventService.Instance.ShowItemsShop.RemoveListener(shopController.SetItemsShop);
    }
}
