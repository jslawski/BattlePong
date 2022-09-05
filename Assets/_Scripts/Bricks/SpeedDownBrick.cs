using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDownBrick : Brick
{
    // Start is called before the first frame update
    void Start()
    {
        this.ability = new SpeedChangeCommand(0.5f);
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
