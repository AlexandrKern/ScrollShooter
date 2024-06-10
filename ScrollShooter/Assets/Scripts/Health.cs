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
            isPlayAudio = true;
            AudioManager.instance.PlayEffect("PlayerDeath");
        }
        if (gameObject.CompareTag("Enemy")&&!isPlayAudio)
        {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            AudioManager.instance.PlayEffect("EnemyDeath");
        }
        StartCoroutine(DeathTime());
    }

    private IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
