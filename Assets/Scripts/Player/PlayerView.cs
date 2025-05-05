using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
