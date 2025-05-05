using System.Collections.Generic;

public class ShopModel
{
    public int carryWeight = 100;
    public List<ItemModel> items;
    public ShopModel()
    {
        items = new List<ItemModel>();
    }
}
