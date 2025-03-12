using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float jumpForce = 10f; // Zıplama kuvveti

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f); // Önceki düşüş hızını sıfırla
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Yukarı kuvvet uygula
            }
        }
    }
}