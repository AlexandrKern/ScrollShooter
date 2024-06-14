using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdPatrol : EnemyBase
{
    public float speed = 2f;
    public float patrolDistance = 5f;
    public float chaseDistance = 3f;
    public float acceleration = 2f;
    public int damage = 10;
    public GameObject hitEffectPrefab;
    public GameObject deathEffectPrefab;

    private Vector2 startPosition;
    private bool movingRight = true;

    private bool isBirdAudioEffect;
    private bool playerInRange = false;
    private bool localIsDeath;
    private Health health;

    protected override void Start()
    {
        isBirdAudioEffect = true;  
        localIsDeath = false;
        health = GetComponent<Health>();
        startPosition = transform.position;

        if (health == null)
        {
            Debug.LogError("Health component missing on the enemy!");
        }
    }

    protected override void Update()
    {
        if (!health.isDeath)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            playerInRange = distanceToPlayer <= chaseDistance;

            if (playerInRange)
            {
                
                Attack();
            }
            else
            {
                Patrol();
            }
        }
        CheckDeath();
    }

    protected override void Patrol()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
            {
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
            {
                Flip();
            }
        }
    }

    protected override void Attack()
    {
        if (isBirdAudioEffect)
        {
            AudioManager.instance.PlayEffect("BirdEffect");
            isBirdAudioEffect = false;
        }
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float currentSpeed = speed + acceleration * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);

        transform.localScale = new Vector3(Mathf.Sign(player.transform.position.x - transform.position.x), 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hitEffectPrefab != null)
            {
                AudioManager.instance.PlayEffect("Smoke");
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }

            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    protected override void CheckDeath()
    {
        if (health.isDeath && !localIsDeath)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            localIsDeath = true;
            Destroy(gameObject);
        }
    }

    protected override void Flip()
    {
        movingRight = !movingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // меняем направление движения путем отражения по оси X
        transform.localScale = theScale;
    }
}
