using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void TriggerAbility(Player ballOwner)
    {

    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Player ballOwner = collision.gameObject.GetComponent<Ball>().owningPlayer;
            this.TriggerAbility(ballOwner);
            Destroy(this.gameObject);
        }
    }
}
