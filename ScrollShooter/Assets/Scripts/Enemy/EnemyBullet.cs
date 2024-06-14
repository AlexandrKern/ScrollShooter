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
        // Уничтожаем пулю по истечении времени жизни
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Перемещаем пулю в заданном направлении
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        // Устанавливаем направление движения пули
        direction = newDirection.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Создаем эффект попадания
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        // Наносим урон объекту, если у него есть компонент Health
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        // Воспроизводим звук взрыва пули
        AudioManager.instance.PlayEffect("EnemyBulletExplosion");

        // Уничтожаем пулю
        Destroy(gameObject);
    }
}
