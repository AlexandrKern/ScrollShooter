using UnityEngine;

public class EnemyBirdPatrol : EnemyBase
{
    public float chaseDistance = 3f;
    public float acceleration = 2f;
    public GameObject hitEffectPrefab;
    public GameObject deathEffectPrefab;


    private bool isBirdAudioEffect;
    private bool playerInRange = false;
    private bool localIsDeath;
    private Health health;

    protected override void Start()
    {
        isBirdAudioEffect = true;
        localIsDeath = false;
        health = GetComponent<Health>();

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
        }
        CheckDeath();
    }

    protected override void Patrol()
    {
    }

    protected override void Attack()
    {
        if (isBirdAudioEffect)
        {
            AudioManager.instance.PlayEffect("BirdEffect");
            isBirdAudioEffect = false;
        }
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float currentSpeed = acceleration * Time.deltaTime;
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
}
