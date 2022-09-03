using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public virtual PlayerCommand HandleInput(float currentPlayerSpeed)
    {
        return null;
    }
}
