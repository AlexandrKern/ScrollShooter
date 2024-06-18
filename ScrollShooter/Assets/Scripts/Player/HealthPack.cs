using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.PlayerHealth(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
