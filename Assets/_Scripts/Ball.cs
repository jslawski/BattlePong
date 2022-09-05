using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{    
    public Player owningPlayer = Player.None;
    public Player opponent = Player.None;

    private Vector3 moveDirection = Vector3.zero;
    private float moveSpeed = 10f;
    private float ballCollisionSpeedIncrement = 1f;
    private float paddleHitSpeedIncrement = 0.5f;


    private float minXDirectionMagnitude = 1.0f;

    public void SetupBall(Player ballOwner)
    {
        this.SetPlayerOwner(ballOwner);

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
                return;                
        }
    }

    private void Update()
    {
        if (Mathf.Abs(this.moveDirection.x) < this.minXDirectionMagnitude)
        {
            if (this.moveDirection.x > 0)
            {
                this.moveDirection = new Vector3(this.minXDirectionMagnitude, this.moveDirection.y, this.moveDirection.z);
            }
            else
            {
                this.moveDirection = new Vector3(-this.minXDirectionMagnitude, this.moveDirection.y, this.moveDirection.z);
            }
        }
    }

    private void FixedUpdate()
    {
        this.transform.Translate(this.moveDirection.normalized * this.moveSpeed * Time.fixedDeltaTime);
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
    /// Also increases the speed of the ball
    /// </summary>
    /// <param name="collision"></param>
    private void HandleBallPaddleCollision(Collision collision)
    {
        Vector3 reflectionVector = GetPaddleReflectionVector(collision.collider, collision.contacts[0].point);
        this.moveDirection = reflectionVector;
        this.moveSpeed += this.paddleHitSpeedIncrement;
    }

    private void SetPlayerOwner(Player newPlayer)
    {
        this.owningPlayer = newPlayer;

        MeshRenderer ballMesh = this.gameObject.GetComponent<MeshRenderer>();

        switch (this.owningPlayer)
        {
            case Player.Player1:
                this.opponent = Player.Player2;
                ballMesh.material = Resources.Load<Material>("Materials/Player1");
                break;
            case Player.Player2:
                this.opponent = Player.Player1;
                ballMesh.material = Resources.Load<Material>("Materials/Player2");
                break;
            default:
                Debug.LogError("Unknown player ID: " + this.opponent + " Unable to re-assign opponent.");
                break;
        }
    }

    private void HandleBallGoalCollision()
    {
        GameManager.instance.RespawnBall(this.owningPlayer);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            this.HandleBallBallCollision(collision.contacts[0].normal);
        }
        else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Brick")
        {
            this.HandlePerfectReflectionCollision(collision.contacts[0].normal);
        }
        else if (collision.gameObject.tag == "Paddle")
        {
            this.HandleBallPaddleCollision(collision);
        }
        else if (collision.gameObject.tag == "Goal")
        {
            this.HandleBallGoalCollision();
        }
    }
}
