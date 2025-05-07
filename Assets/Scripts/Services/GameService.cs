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
        int i = UnityEngine.Random.Range(0, itemScriptableObjects.Length);
        ItemScriptableObject item = itemScriptableObjects[i];

        ItemModel newItem = new ItemModel(item, ItemInventoryType.NONE);

        playerController.setCoins(newItem);
        playerController.AddItems(newItem);
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
