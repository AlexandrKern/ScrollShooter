using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f;
    public float patrolSpeed = 2f;

    protected enum State { Patrolling, Attacking }
    protected State currentState = State.Patrolling;
    protected bool isMovingRight = true;



    protected virtual void Update()
    {

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                currentState = State.Attacking;
            }
            else
            {
                currentState = State.Patrolling;
            }

            switch (currentState)
            {
                case State.Patrolling:
                    Patrol();
                    break;
                case State.Attacking:
                    Attack();
                    break;
            }
        Death();

    }

    protected abstract void Patrol();
    protected abstract void Attack();

    protected abstract void Death();

    protected void Flip()
    {
        isMovingRight = !isMovingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
