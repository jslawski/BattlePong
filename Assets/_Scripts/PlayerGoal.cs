using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoal : MonoBehaviour
{    
    public Player owningPlayer = Player.None;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Ball collidedBall = collision.gameObject.GetComponent<Ball>();

            if (collidedBall.owningPlayer != this.owningPlayer)
            {
                GameManager.instance.UpdateLives(collidedBall.opponent);
            }
        }
    }
}
