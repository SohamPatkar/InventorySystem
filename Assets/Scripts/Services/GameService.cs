using System;
using UnityEngine;

public class GameService : MonoBehaviour
{
    private static GameService instance;
    public static GameService Instance { get { return instance; } }
    [SerializeField] private GameObject shopView;
    [SerializeField] private GameObject playerView;
    [SerializeField] private GameObject inventoryPanel;
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
        uIView.Initialize();
        CreateShopItems();
    }

    private void CreateShop()
    {
        shopController = new ShopController(shopView.GetComponent<ShopView>(), inventoryPanel.transform);
    }

    public void CreateShopItems()
    {
        foreach (ItemScriptableObject item in itemScriptableObjects)
        {
            ItemModel newItem = new ItemModel(item);
            newItem.itemInventoryType = ItemInventoryType.SHOPINVENTORY;
            shopController.AddItem(newItem);
        }
    }

    public void CreatePlayerItems(GameObject panel)
    {

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
