using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : PlayerCommand
{
    private Vector3 moveDirection = Vector3.zero;
    private float moveSpeed = 0f;

    private float maxYBound = 6f;

    public MoveCommand(Vector3 direction, float speed)
    {
        this.moveDirection = direction;
        this.moveSpeed = speed;
    }

    public override void Execute(GameObject targetObject)
    {
        if (Mathf.Abs(targetObject.transform.position.y) <= this.maxYBound)
        {
            targetObject.transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        if (targetObject.transform.position.y > this.maxYBound)
        {
            targetObject.transform.position = 
                new Vector3(targetObject.transform.position.x, 
                this.maxYBound, 
                targetObject.transform.position.z);
        }

        if (targetObject.transform.position.y < -this.maxYBound)
        {
            targetObject.transform.position =
                new Vector3(targetObject.transform.position.x,
                -this.maxYBound,
                targetObject.transform.position.z);
        }
    }
}
