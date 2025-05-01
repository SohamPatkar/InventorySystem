using System;
using UnityEngine;

public class GameService : MonoBehaviour
{
    private static GameService instance;
    public static GameService Instance { get { return instance; } }
    [SerializeField] private GameObject shopView;
    [SerializeField] private GameObject playerView;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private ItemView itemView;
    [SerializeField] private ItemScriptableObject[] itemScriptableObjects;
    [SerializeField] private UIView uIView;
    private PlayerController playerController;
    private ShopController shopController;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        CreatePlayer();
        CreateShop();
        uIView.InitializeVariables();
        CreateShopItems(shopController.GetShopInventory());
    }

    private void CreateShop()
    {
        shopController = new ShopController(shopView.GetComponent<ShopView>(), inventoryPanel.transform);
    }

    public void CreateShopItems(GameObject panel)
    {
        foreach (ItemScriptableObject item in itemScriptableObjects)
        {
            ItemController newItem = new ItemController(item, itemView, panel);
            newItem.SetItemInventory(ItemInventoryType.SHOPINVENTORY);
            shopController.AddItem(newItem);
        }
    }

    public void CreatePlayerItems(GameObject panel)
    {
        foreach (ItemScriptableObject item in itemScriptableObjects)
        {
            ItemController newItem = new ItemController(item, itemView, panel);

            EventService.Instance.AddPlayerItems.InvokeEvent(newItem);
            playerController.AddItems(newItem);
            EventService.Instance.UpdateUICoins.InvokeEvent();
        }
    }

    private void CreatePlayer()
    {
        playerController = new PlayerController(playerView.GetComponent<PlayerView>(), inventoryPanel);
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    public ShopController GetShopController()
    {
        return shopController;
    }
}
