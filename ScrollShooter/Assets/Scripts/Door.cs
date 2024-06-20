using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public GameObject openDoorEffect;
    public Text doormessage;
    private int keyValue;
    public Image [] keysImages;
    private int raisedKey;
    private bool isComeToTheDoor;

    private void Start()
    {
        raisedKey = 0;
        keyValue = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isComeToTheDoor = true;
        }
    }
    private void Update()
    {
        if (isComeToTheDoor)
        {
            OpenDoor();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isComeToTheDoor = false;
            doormessage.text = "";
        }
    }
    public void KeysOnScreen()
    {

        keysImages[raisedKey].color = Color.white;
        keysImages[raisedKey].transform.localScale *= 2;
        StartCoroutine(ReducesKey());
        raisedKey++;
    }
    private void OpenDoor()
    {
        if (keyValue == 6)
        {
            doormessage.text = $"Ðress the ' Å ' key";
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.instance.PlayEffect("DoorEffect");
                Instantiate(openDoorEffect,transform.position,transform.rotation);
                Destroy(gameObject);
            }

        }
        else
        {
            doormessage.text = $"Òhere are not enough keys ' {6 - keyValue} '";
        }
    }
    public void SetKey()
    {
        keyValue++; 
    }

    private IEnumerator ReducesKey()
    {
        yield return new WaitForSeconds(2);
        keysImages[raisedKey-1].transform.localScale /= 2;
    }
}
