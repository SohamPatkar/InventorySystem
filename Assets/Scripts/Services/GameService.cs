using System;
using UnityEngine;

public class GameService : MonoBehaviour
{
    private static GameService instance;
    public static GameService Instance { get { return instance; } }
    [SerializeField] private GameObject shopView;
    [SerializeField] private GameObject playerView;
    [SerializeField] private GameObject inventoryPanel;

    private PlayerController playerController;
    private ShopController shopController;
    private UIView uIView;
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
        CreateUIView();
    }

    private void CreateShop()
    {
        shopController = new ShopController(shopView.GetComponent<ShopView>(), inventoryPanel.transform);
    }

    private void CreatePlayer()
    {
        playerController = new PlayerController(playerView.GetComponent<PlayerView>(), inventoryPanel);
    }

    private void CreateUIView()
    {
        uIView = new UIView();
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
