using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;

    public void SetHealth(float healthNormalized)
    {
        healthBarFill.fillAmount = healthNormalized;
    }
}
