using UnityEngine;

public class MoveAndTurn : MonoBehaviour
{
    public float speed = 2f;
    private int direction = -1; // Ba�lang��ta sola hareket

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            direction *= -1; // Y�n� tersine �evir
        }
    }
}
