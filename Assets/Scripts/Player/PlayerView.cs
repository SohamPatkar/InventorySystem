using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {

    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void OnDisable()
    {

    }
}
