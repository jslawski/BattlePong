using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public AudioClip audioClip;

    protected PowerUpCommand ability = null;

    protected virtual void TriggerAbility(Ball triggeredBall)
    {
        AudioSource.PlayClipAtPoint(this.audioClip, Vector3.zero);
    }

    private void Start()
    {
        this.audioClip = Resources.Load<AudioClip>("Audio/BrickHit");
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {            
            this.TriggerAbility(collision.gameObject.GetComponent<Ball>());
            Destroy(this.gameObject);
        }
    }
}
