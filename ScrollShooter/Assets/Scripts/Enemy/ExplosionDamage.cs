using System.Collections;
using System.Collections.Generic;
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
        Destroy(gameObject, duration); // Удалить объект через duration секунд
    }

    void Update()
    {
        if (playerInRange)
        {
            timer += Time.deltaTime;

            if (timer >= damageInterval)
            {
                DealDamage(collider);
                timer = 0f; // Сброс таймера
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
        // Здесь вы должны добавить логику нанесения урона игроку
        // Например, если у вашего игрока есть компонент Health, то:
        Health playerHealth = collider.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
