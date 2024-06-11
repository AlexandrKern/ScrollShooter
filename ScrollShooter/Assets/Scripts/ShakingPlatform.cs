using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingPlatform : MonoBehaviour
{
    public float shakeDuration = 1f; // Длительность тряски
    public float shakeMagnitude = 0.1f; // Сила тряски
    public float fallDelay = 1f; // Задержка перед падением
    public float respawnDelay = 5f; // Задержка перед возвращением на исходное место
    public float fallSpeed = 10f; // Начальная скорость падения
    public float gravityScale = 5f; // Масштаб гравитации для быстрого падения

    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private bool isFalling = false;
    private bool isShaking = false;

    void Start()
    {
        initialPosition = transform.localPosition;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Начальное состояние кинематическое
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isShaking)
        {
            StartCoroutine(ShakeAndFall());
        }
    }

    IEnumerator ShakeAndFall()
    {
        isShaking = true;

        // Трясем платформу
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ждем перед падением
        yield return new WaitForSeconds(fallDelay);

        // Отключаем коллайдер и устанавливаем Rigidbody2D в Dynamic для падения
        col.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale; // Устанавливаем увеличенную гравитацию для быстрого падения
        rb.velocity = new Vector2(0, -fallSpeed); // Устанавливаем начальную скорость падения
        isFalling = true;
    }

    void OnBecameInvisible()
    {
        if (isFalling)
        {
            // Делаем платформу невидимой и неактивной
            spriteRenderer.enabled = false;
            col.enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0;
            isFalling = false;
            isShaking = false;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        // Возвращаем платформу на исходное место и делаем её видимой и активной
        transform.localPosition = initialPosition;
        spriteRenderer.enabled = true;
        col.enabled = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
