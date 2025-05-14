public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public GameEventController ShowItemsShop { get; private set; }
    public GameEventController<int> UpdateWeight { get; private set; }
    public GameEventController<int> UpdateCoins { get; private set; }
    public GameEventController OnExceedWeight { get; private set; }
    public GameEventController NotEnoughCoinsText { get; private set; }
    public GameEventController ItemBoughtText { get; private set; }
    public GameEventController ItemSoldText { get; private set; }
    public GameEventController<ItemModel> OpenBuyPanel { get; private set; }
    public EventService()
    {
        NotEnoughCoinsText = new GameEventController();
        UpdateWeight = new GameEventController<int>();
        UpdateCoins = new GameEventController<int>();
        ShowItemsShop = new GameEventController();
        OnExceedWeight = new GameEventController();
        OpenBuyPanel = new GameEventController<ItemModel>();
        ItemBoughtText = new GameEventController();
        ItemSoldText = new GameEventController();
    }
}
