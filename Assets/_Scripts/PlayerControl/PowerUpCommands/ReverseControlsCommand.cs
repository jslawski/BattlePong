using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControlsCommand : PowerUpCommand
{
    public override void Execute(Player targetPlayer)
    {
        GameManager.instance.ReverseControlsOfPlayer(targetPlayer);
    }
}
