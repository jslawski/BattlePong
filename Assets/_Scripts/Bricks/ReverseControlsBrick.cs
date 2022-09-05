using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControlsBrick : Brick
{
    void Start()
    {
        this.ability = new ReverseControlsCommand();
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
