using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int coins;
    public int carryWeight;
    public List<ItemModel> items;

    public PlayerModel()
    {
        items = new List<ItemModel>();
    }
}
