using UnityEngine;
using UnityEngine.UI;

public class BulletScale : MonoBehaviour
{
    public Image bulletBarFill;

    public void SetBullet(float bulletNormalized)
    {
        bulletBarFill.fillAmount = bulletNormalized;
    }
}
