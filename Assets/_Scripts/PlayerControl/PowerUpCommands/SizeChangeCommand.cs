using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeCommand : PowerUpCommand
{
    private float sizeChange = 0f;

    public SizeChangeCommand(float sizeIncrement)
    {
        this.sizeChange = sizeIncrement;
    }

    public override void Execute(Player targetPlayer)
    {
        GameManager.instance.ChangeSizeOfPlayer(targetPlayer, this.sizeChange);
    }
}
