using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int coins = 0;
    public int maxCarryWeight = 100;
    public int carryWeight = 0;
    public List<ItemModel> items;

    public PlayerModel()
    {
        items = new List<ItemModel>();
    }
}
