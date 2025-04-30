using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coins;
    private GameObject playerInventory, shopInventory;

    private void Start()
    {
        EventService.Instance.AddPlayerItems.AddListener(UpdatePlayerItems);
        EventService.Instance.UpdateUICoins.AddListener(SetCoins);
    }

    public void InitializeVariables()
    {
        playerInventory = GameService.Instance.GetPlayerController().GetPlayerInventory();
        shopInventory = GameService.Instance.GetShopController().GetShopInventory();
        SetCoins();
    }

    public void SetCoins()
    {
        coins.text = "Coins: " + GameService.Instance.GetPlayerController().GetPlayerCoins();
    }

    public void UpdatePlayerItems(ItemController item)
    {
        item.itemView.gameObject.transform.SetParent(playerInventory.transform);
    }

    public void GenerateItemsPlayer()
    {
        GameService.Instance.CreatePlayerItems(playerInventory);
    }

}
