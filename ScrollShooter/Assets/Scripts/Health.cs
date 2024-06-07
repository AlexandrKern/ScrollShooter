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

    void Start()
    {
        isPlayAudio = false;
        isDeath = false;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("damage");
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
            isPlayAudio = true;
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
