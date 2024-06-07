using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public GameObject hitEffectPrefab;
    public int damage = 10;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Создаем эффект попадания
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        // Наносим урон игроку
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
        AudioManager.instance.PlayEffect("EnemyBulletExplosion");
        // Уничтожаем пулю
        Destroy(gameObject);
    }
}
