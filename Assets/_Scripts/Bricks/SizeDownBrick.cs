using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDownBrick : Brick
{
    void Start()
    {
        this.ability = new SizeChangeCommand(1.0f);

        this.audioClip = Resources.Load<AudioClip>("Audio/PowerUpHit");
    }

    protected override void TriggerAbility(Ball triggeredBall)
    {
        if (this.ability != null)
        {
            this.ability.Execute(triggeredBall.opponent);

            GameManager.instance.SpawnParticleSignal(this.transform.position, triggeredBall.owningPlayer, triggeredBall.opponent);
        }

        base.TriggerAbility(triggeredBall);
    }
}
