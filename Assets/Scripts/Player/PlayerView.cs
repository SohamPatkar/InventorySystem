using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

}
