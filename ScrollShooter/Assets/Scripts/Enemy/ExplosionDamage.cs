using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public int damage = 10;
    public float duration = 2f;
    public float damageInterval = 0.5f;

    private float timer;
    private bool playerInRange;

    private Collider2D collider;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        if (playerInRange)
        {
            timer += Time.deltaTime;

            if (timer >= damageInterval)
            {
                DealDamage(collider);
                timer = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collider = other;
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collider = other;
            playerInRange = false;
        }
    }

    void DealDamage(Collider2D collider)
    {
        Health playerHealth = collider.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
