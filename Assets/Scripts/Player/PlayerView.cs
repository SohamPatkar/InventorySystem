using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerController.GetItemsList().Count);
    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
