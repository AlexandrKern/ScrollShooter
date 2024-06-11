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
        if(healthBar != null)
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
        if (gameObject.CompareTag("Player")&&!isPlayAudio)
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            isPlayAudio = true;
            AudioManager.instance.PlayEffect("PlayerDeath");
        }
        if (gameObject.CompareTag("Enemy")&&!isPlayAudio)
        {
            if (playerScore != null)
            {
                playerScore.AddScore(points);
            }
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            AudioManager.instance.PlayEffect("EnemyDeath");
        }
        StartCoroutine(DeathTime());
    }

    private IEnumerator DeathTime()
    {
       
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }

    public void PlayerHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        healthBar.SetHealth((float)currentHealth / maxHealth);
    }
}
