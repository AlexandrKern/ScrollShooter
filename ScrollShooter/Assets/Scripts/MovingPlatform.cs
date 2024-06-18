using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f;
    public float horizontalMoveDistance = 5f;
    public float verticalMoveDistance = 3f;
    public bool moveHorizontally = true; 
    public bool moveVertically = false;

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
