using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletedBulit : MonoBehaviour
{
    public GameObject bulitCollisionPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bulit")) 
        {

            Instantiate(bulitCollisionPrefab,collision.transform.position,collision.transform.rotation);

            Destroy(collision.gameObject);
        }

    }
}
