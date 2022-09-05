using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallCommand : PowerUpCommand
{
    Vector3 spawnPosition = Vector3.zero;

    public MultiBallCommand(Vector3 spawnPos)
    {
        this.spawnPosition = spawnPos;
    }

    public override void Execute(Player targetPlayer)
    {
        GameManager.instance.DuplicateBall(targetPlayer, this.spawnPosition);
    }
}
