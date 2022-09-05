using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpBrick : Brick
{
    // Start is called before the first frame update
    void Start()
    {
        this.ability = new SpeedChangeCommand(1.5f);
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
