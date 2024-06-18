using System.Collections;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public GameObject firePrefab;
    public float fireDuration = 2.0f;
    public float pauseDuration = 3.0f; 

    private IEnumerator Start()
    {
        while (true)
        {
            yield return StartCoroutine(PlayFire());
            yield return StartCoroutine(StopFire());
        }
    }

    private IEnumerator PlayFire()
    {
        yield return new WaitForSeconds(pauseDuration);
        firePrefab.SetActive(true);
    }

    private IEnumerator StopFire()
    {
        yield return new WaitForSeconds(fireDuration);
        firePrefab.SetActive(false);
    }


}
