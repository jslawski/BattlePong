using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    protected PowerUpCommand ability = null;

    protected virtual void TriggerAbility(Ball triggeredBall){}

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {            
            this.TriggerAbility(collision.gameObject.GetComponent<Ball>());
            Destroy(this.gameObject);
        }
    }
}
