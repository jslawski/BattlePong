using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeCommand : PowerUpCommand
{
    private float speedChange = 0f;

    public SpeedChangeCommand(float speedIncrement)
    {
        this.speedChange = speedIncrement;
    }

    public override void Execute(Player targetPlayer)
    {
        GameManager.instance.ChangeSpeedOfPlayer(targetPlayer, this.speedChange);
    }


}
