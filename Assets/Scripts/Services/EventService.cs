using UnityEngine;

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


    public GameEventController<ItemController> AddPlayerItems { get; private set; }

    public EventService()
    {
        AddPlayerItems = new GameEventController<ItemController>();
    }
}
