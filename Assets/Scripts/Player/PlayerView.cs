using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        EventService.Instance.AddPlayerItems.AddListener(playerController.SetPlayerCoins);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.GetItemsList().Count > 0)
        {
            foreach (ItemController item in playerController.GetItemsList())
            {
                Debug.Log(item.GetItemId());
            }
        }
    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
