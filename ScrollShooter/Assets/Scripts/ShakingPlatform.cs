using System.Collections;

using UnityEngine;

public class ShakingPlatform : MonoBehaviour
{
    public float shakeDuration = 1f; 
    public float shakeMagnitude = 0.1f; 
    public float fallDelay = 1f; 
    public float respawnDelay = 5f; 
    public float fallSpeed = 10f; 
    public float gravityScale = 5f; 

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
        rb.bodyType = RigidbodyType2D.Kinematic;
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

        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(fallDelay);

        col.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale; 
        rb.velocity = new Vector2(0, -fallSpeed); 
        isFalling = true;
    }

    void OnBecameInvisible()
    {
        if (isFalling)
        {
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

        transform.localPosition = initialPosition;
        spriteRenderer.enabled = true;
        col.enabled = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
