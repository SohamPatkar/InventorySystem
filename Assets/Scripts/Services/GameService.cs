using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameService : MonoBehaviour
{
    private static GameService instance;
    public static GameService Instance { get { return instance; } }
    [SerializeField] private GameObject shopView;
    [SerializeField] private GameObject playerView;

    //this is the panel that will hold the player and shopview
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private ItemScriptableObject[] itemScriptableObjects;
    [SerializeField] private UIView uIView;

    [Header("Sound System")]
    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioSource sfxSource;
    private SoundManager soundManager;
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
        CreateSoundManager();
        uIView.Initialize();
        CreateShopItems();
    }

    private void CreateShop()
    {
        shopController = new ShopController(shopView.GetComponent<ShopView>(), inventoryPanel.transform);
    }

    private void CreateShopItems()
    {
        foreach (ItemScriptableObject item in itemScriptableObjects)
        {
            ItemModel newItem = new ItemModel(item, ItemInventoryType.SHOPINVENTORY);
            shopController.AddItem(newItem);
        }
    }

    private void CreateSoundManager()
    {
        soundManager = new SoundManager(sfxSource, sounds);
    }

    public void CreatePlayerItems(GameObject panel)
    {
        ItemRarity typeOfItemToSpawn = GetItemTypeByRarity(GetPlayerInventoryValue());

        List<ItemScriptableObject> filteredItems = itemScriptableObjects.Where(item => item.itemRarity == typeOfItemToSpawn).ToList();

        if (filteredItems.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, filteredItems.Count);
            ItemScriptableObject itemToSpawn = filteredItems[randomIndex];

            ItemModel newItem = new ItemModel(itemToSpawn, ItemInventoryType.NONE);
            playerController.AddItems(newItem);
        }
    }

    private int GetPlayerInventoryValue()
    {
        int valueOfPlayerInventory = 0;

        List<ItemModel> playerInventoryList = playerController.GetItemsList();

        foreach (ItemModel item in playerInventoryList)
        {
            valueOfPlayerInventory += item.sellingPrice * item.quantity;
        }

        return valueOfPlayerInventory;
    }

    private ItemRarity GetItemTypeByRarity(int playerCoins)
    {
        if (playerCoins < 25)
        {
            return ItemRarity.VERYCOMMON;
        }
        else if (playerCoins < 50)
        {
            return ItemRarity.COMMON;
        }
        else if (playerCoins < 100)
        {
            return ItemRarity.RARE;
        }
        else if (playerCoins < 125)
        {
            return ItemRarity.EPIC;
        }
        else
        {
            return ItemRarity.LEGENDARY;
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

    public SoundManager GetSoundManager()
    {
        return soundManager;
    }

    public ShopController GetShopController()
    {
        return shopController;
    }
}
