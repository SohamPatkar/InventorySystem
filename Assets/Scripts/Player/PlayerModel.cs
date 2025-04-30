using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int coins = 25;
    public int carryWeight = 50;
    public List<ItemController> items;

    public PlayerModel()
    {
        items = new List<ItemController>();
    }
}
