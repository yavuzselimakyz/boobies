using UnityEngine;

public class Kunaidirection : MonoBehaviour
{
    public float stickDepth = 0.05f;  // Duvara ne kadar gömülsün
    public string[] targetTags = { "Wall", "Enemy" };  // Hangi tag'lere saplanacak
    private bool stuck = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (stuck) return;

        foreach (string tag in targetTags)
        {
            if (other.CompareTag(tag))
            {
                StickToTarget(other);
                break;
            }
        }
    }

    void StickToTarget(Collider2D other)
    {
        stuck = true;

        // Kunai'nin ucunu biraz ileri al, böylece duvara saplanmış gibi görünür
        transform.position += transform.right * stickDepth;

        // Rigidbody'yi durdur ve pasifleştir
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        rb.simulated = false;
    }
}
