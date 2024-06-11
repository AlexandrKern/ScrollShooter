using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public GameObject firePrefab; // Префаб огня
    public float fireDuration = 2.0f; // Длительность периода стрельбы
    public float pauseDuration = 3.0f; // Длительность периода паузы

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
