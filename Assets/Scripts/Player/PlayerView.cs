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

    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
