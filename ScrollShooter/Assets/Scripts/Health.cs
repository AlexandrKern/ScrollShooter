using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    public bool isDeath;
    private bool isPlayAudio;
    public HealthBar healthBar;
    public int points = 10; // Количество очков, которые дает враг при уничтожении
    private PlayerScore playerScore;

    void Start()
    {
        isPlayAudio = false;
        isDeath = false;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetHealth(1f);
        }

        // Предполагаем, что объект игрока имеет тег "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScore = player.GetComponent<PlayerScore>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("damage");

        if (gameObject.CompareTag("Player"))
        {
            healthBar.SetHealth((float)currentHealth / maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("death");
        isDeath = true;

        if (!isPlayAudio)
        {
            if (gameObject.CompareTag("Player"))
            {
                HandlePlayerDeath();
            }
            else if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("EnemyBird"))
            {
                HandleEnemyDeath();
            }

            isPlayAudio = true;
        }

        StartCoroutine(DeathTime());
    }

    private void HandlePlayerDeath()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        AudioManager.instance.PlayEffect("PlayerDeath");
    }

    private void HandleEnemyDeath()
    {
        if (playerScore != null)
        {
            playerScore.AddScore(points);
        }

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        if (gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlayEffect("EnemyDeath");
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        if (gameObject.CompareTag("EnemyBird"))
        {
            AudioManager.instance.PlayEffect("BirdDeathEffect");
        }
    }

    private IEnumerator DeathTime()
    {
        if (gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(2); // Загрузите сцену заново или замените на нужный индекс сцены
        }
        else
        {
            yield return new WaitForSeconds(0); // Можно заменить на задержку, если нужно
            Destroy(gameObject);
        }
    }

    public void PlayerHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth((float)currentHealth / maxHealth);
    }
}
