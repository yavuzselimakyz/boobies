using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float jumpForce = 10f; // Zıplama kuvveti

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Sadece oyuncuya etki etsin
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Önce dikey hızı sıfırla (daha tutarlı zıplama için)
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Yukarı doğru kuvvet uygula
            }
        }
    }
}