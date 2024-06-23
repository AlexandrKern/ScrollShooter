using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    public bool isDeath;
    private bool isPlayAudio;
    public HealthBar healthBar;

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
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (gameObject.CompareTag("Player")|| gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("damage");
        }
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
        if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("death");
        }
        
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

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        if (gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlayEffect("EnemyDeath");
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
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
            SceneManager.LoadScene(2);
        }
        else
        {
            yield return new WaitForSeconds(0);
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
