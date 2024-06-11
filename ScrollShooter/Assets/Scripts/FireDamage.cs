using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damage = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        // Наносим урон игроку
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
