using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Reload(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}
