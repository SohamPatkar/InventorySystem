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

    public GameEventController ShowItemsUI { get; private set; }
    public GameEventController ShowErrorText { get; private set; }
    public GameEventController<ItemModel> OpenBuyPanel { get; private set; }
    public GameEventController<ItemType> TabPressed { get; private set; }
    public EventService()
    {
        ShowItemsUI = new GameEventController();
        ShowErrorText = new GameEventController();
        OpenBuyPanel = new GameEventController<ItemModel>();
        TabPressed = new GameEventController<ItemType>();
    }
}
