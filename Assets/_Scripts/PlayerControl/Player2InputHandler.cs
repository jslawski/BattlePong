using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2InputHandler : InputHandler
{
    public override PlayerCommand HandleInput(float currentPlayerSpeed)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return new MoveCommand(Vector3.up, currentPlayerSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            return new MoveCommand(Vector3.down, currentPlayerSpeed);
        }

        return null;
    }
}
