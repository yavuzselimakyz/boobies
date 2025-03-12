using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public GameObject targetObject;
    public float moveSpeed = 2f;
    public float minY = -5f;
    private Vector3 initialPosition;
    private bool shouldMove = false;

    private void Start()
    {
        if (targetObject != null)
        {
            initialPosition = targetObject.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldMove = true;
        }
    }

    private void Update()
    {
        if (shouldMove && targetObject != null)
        {
            if (targetObject.transform.position.y > minY)
            {
                targetObject.transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            }
            else
            {
                shouldMove = false;
            }
        }
        else if (GameObject.FindGameObjectWithTag("Player") == null && targetObject.transform.position.y < initialPosition.y)
        {
            targetObject.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }
}
