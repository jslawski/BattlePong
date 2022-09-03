using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1InputHandler : InputHandler
{
    public override PlayerCommand HandleInput(float currentPlayerSpeed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(Vector3.up, currentPlayerSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(Vector3.down, currentPlayerSpeed);
        }

        return null;
    }
}
