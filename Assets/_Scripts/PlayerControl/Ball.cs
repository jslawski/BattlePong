using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Player owningPlayer = Player.None;

    private Vector3 moveDirection = Vector3.zero;
    private float moveSpeed = 5f;
    private float ownerSpeedIncrement = 0.1f;
    private float ballCollisionSpeedIncrement = 1f;


    public void SetupBall(Player ballOwner)
    {
        switch (ballOwner)
        {
            case Player.Player1:
                this.moveDirection = new Vector3(-1.0f, -0.5f, 0.0f);
                break;
            case Player.Player2:
                this.moveDirection = new Vector3(1.0f, -0.5f, 0.0f);
                break;
            default:
                Debug.LogError("Unknown Player: " + ballOwner + ", unable to setup ball.");
                break;
        }
    }
   
    private void FixedUpdate()
    {
        this.transform.Translate(this.moveDirection * this.moveSpeed * Time.fixedDeltaTime);
    }

    private Vector3 GetPerfectReflectionVector(Vector3 collisionNormal)
    {
        return (this.moveDirection - (2 * Vector3.Dot(this.moveDirection, collisionNormal) * collisionNormal));
    }

    private Vector3 GetPaddleReflectionVector(Collider paddleCollider, Vector3 collisionPoint)
    {
        Vector3 verticalReflectionDirection = (paddleCollider.transform.position.y < collisionPoint.y) ? Vector3.up : Vector3.down;
        float verticalReflectionRatio = (Mathf.Abs(paddleCollider.transform.position.y - collisionPoint.y) / paddleCollider.bounds.extents.y);
        verticalReflectionDirection *= verticalReflectionRatio;

        return new Vector3(-this.moveDirection.x, verticalReflectionDirection.y, 0.0f);
    }

    /// <summary>
    /// Called whenever the ball collides with another ball.
    /// Ball should be perfectly reflected and have a large increase in movement speed
    /// </summary>
    /// <param name="collisionNormal"></param>
    private void HandleBallBallCollision(Vector3 collisionNormal)
    {
        this.HandlePerfectReflectionCollision(collisionNormal);
        this.moveSpeed += this.ballCollisionSpeedIncrement;
    }

    /// <summary>
    /// Called whenever the ball collides with an object that causes it to perfectly reflect
    /// (Includes Walls, Bricks, and other Balls)
    /// </summary>
    /// <param name="collisionNormal"></param>
    private void HandlePerfectReflectionCollision(Vector3 collisionNormal)
    {
        Vector3 reflectionVector = this.GetPerfectReflectionVector(collisionNormal);
        this.moveDirection = reflectionVector;
    }

    /// <summary>
    /// Called whenever the ball collides with a player paddle, causing it to reflect in a
    /// direction relative to where on the paddle the collision occurred.
    /// Also increases the speed of the ball if the paddle it collided with was the owner's
    /// </summary>
    private void HandleBallPaddleCollision(Collision collision)
    {
        bool isOwner = (collision.gameObject.GetComponent<PlayerPaddle>().playerNumber == this.owningPlayer);
        Vector3 reflectionVector = GetPaddleReflectionVector(collision.collider, collision.contacts[0].point);
        this.moveDirection = reflectionVector;
        this.moveSpeed += this.ownerSpeedIncrement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Reflect the ball if it collides with another ball or a wall using a perfect reflection vector
        if (collision.gameObject.tag == "Ball")
        {
            this.HandleBallBallCollision(collision.contacts[0].normal);

        }
        else if (collision.gameObject.tag == "Wall")
        {
            this.HandlePerfectReflectionCollision(collision.contacts[0].normal);
        }
        //Reflect the ball if it collides with a player paddle relative to where on the paddle the ball made contact
        else if (collision.gameObject.tag == "Paddle")
        {
            this.HandleBallPaddleCollision(collision);
        }
    }
}
