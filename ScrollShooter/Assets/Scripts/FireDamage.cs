using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damage = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
