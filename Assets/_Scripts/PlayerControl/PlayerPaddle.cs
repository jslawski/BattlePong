using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player { Player1, Player2, None };

public class PlayerPaddle : MonoBehaviour
{
    public Player playerNumber = Player.None;
    
    public float moveSpeed = 5f;

    public int playerLives = 3;

    private PlayerCommand currentFrameCommand = null;

    private InputHandler inputHandler;

    private void Awake()
    {
        switch (this.playerNumber)
        {
            case Player.Player1:
                this.inputHandler = new Player1InputHandler();
                break;
            case Player.Player2:
                this.inputHandler = new Player2InputHandler();
                break;
            default:
                Debug.LogError("Error: Unrecognized Player ID: " + this.playerNumber + ", unable to setup input handler.");
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
