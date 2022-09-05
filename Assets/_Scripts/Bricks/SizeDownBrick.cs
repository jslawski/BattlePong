using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDownBrick : Brick
{
    void Start()
    {
        this.ability = new SizeChangeCommand(0.5f);
    }

    protected override void TriggerAbility(Ball triggeredBall)
    {
        if (this.ability != null)
        {
            this.ability.Execute(triggeredBall.opponent);
        }

        base.TriggerAbility(triggeredBall);
    }
}
