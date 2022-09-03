using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 0;

    [SerializeField]
    private float moveSpeed = 5f;

    private PlayerCommand currentFrameCommand = null;

    private InputHandler inputHandler;

    private void Awake()
    {
        switch (this.playerNumber)
        {
            case 1:
                this.inputHandler = new Player1InputHandler();
                break;
            case 2:
                this.inputHandler = new Player2InputHandler();
                break;
            default:
                Debug.LogError("Error: Unrecognized Player ID: " + this.playerNumber);
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        this.currentFrameCommand = inputHandler.HandleInput(this.moveSpeed);
    }

    private void FixedUpdate()
    {
        if (this.currentFrameCommand != null)
        {
            this.currentFrameCommand.Execute(this.gameObject);
        }
    }
}
