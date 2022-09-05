using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUpBrick : Brick
{
    void Start()
    {
        this.ability = new SizeChangeCommand(3.0f);
    }

    protected override void TriggerAbility(Ball triggeredBall)
    {
        if (this.ability != null)
        {
            this.ability.Execute(triggeredBall.owningPlayer);
        }

        base.TriggerAbility(triggeredBall);
    }
}
