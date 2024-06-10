using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f; // Скорость платформы
    public float horizontalMoveDistance = 5f; // Дистанция горизонтального движения платформы
    public float verticalMoveDistance = 3f; // Дистанция вертикального движения платформы
    public bool moveHorizontally = true; // Перемещение по горизонтали
    public bool moveVertically = false; // Перемещение по вертикали

    private Vector3 startPosition;
    private bool movingPositiveDirection = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (moveHorizontally)
        {
            if (movingPositiveDirection)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (transform.position.x >= startPosition.x + horizontalMoveDistance)
                {
                    movingPositiveDirection = false;
                }
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (transform.position.x <= startPosition.x - horizontalMoveDistance)
                {
                    movingPositiveDirection = true;
                }
            }
        }

        if (moveVertically)
        {
            if (movingPositiveDirection)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                if (transform.position.y >= startPosition.y + verticalMoveDistance)
                {
                    movingPositiveDirection = false;
                }
            }
            else
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                if (transform.position.y <= startPosition.y - verticalMoveDistance)
                {
                    movingPositiveDirection = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
