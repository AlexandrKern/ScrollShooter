using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private Door door;
    public GameObject keyEffect;
    private void Start()
    {
        door = FindObjectOfType<Door>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.SetKey();
            door.KeysOnScreen();
            AudioManager.instance.PlayEffect("KeyEffect");
            Instantiate(keyEffect,transform.position,transform.rotation);
            Destroy(gameObject);
        }
       
    }
}
