using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public GameObject player;
    public float detectionRange = 5f;
    public float patrolSpeed = 2f;

    protected enum State { Patrolling, Attacking }
    protected State currentState = State.Patrolling;
    protected bool isMovingRight = true;
    private Health playerHealth;

    protected virtual void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player is not assigned in the Inspector");
        }

        playerHealth = player.GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("Player does not have a Health component");
        }
    }

    protected virtual void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        currentState = distanceToPlayer <= detectionRange ? State.Attacking : State.Patrolling;

        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Attacking:
                if (playerHealth != null && !playerHealth.isDeath)
                {
                    Attack();
                }
                break;
        }

        CheckDeath();
    }

    protected abstract void Patrol();
    protected abstract void Attack();
    protected abstract void CheckDeath();

    protected virtual void Flip()
    {
        isMovingRight = !isMovingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
