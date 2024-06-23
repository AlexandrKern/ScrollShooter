using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private GameObject impactEffect;
    public int damage = 10;
    public float lifetime = 2f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetImpactEffect(GameObject effect)
    {
        impactEffect = effect;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Health playerHealth = collision.gameObject.GetComponent<Health>();
        if (playerHealth != null && !collision.gameObject.CompareTag("Player"))
        {
            if (!playerHealth.isDeath)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        AudioManager.instance.PlayEffect("Explosion");
        Destroy(gameObject);
    }
}
