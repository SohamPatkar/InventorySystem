using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int coins = 0;
    public int carryWeight = 100;
    public List<ItemModel> items;

    public PlayerModel()
    {
        items = new List<ItemModel>();
    }
}
