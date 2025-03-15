using UnityEngine;

public class Kunai : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool stuck = false; // Kunai'nin saplanıp saplanmadığını kontrol eder
    private TrailRenderer trail;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>(); // Trail Renderer bileşenini al
    }

    private void Update()
    {
        if (!stuck && rb.linearVelocity.sqrMagnitude > 0.01f) // Eğer hareket ediyorsa
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") && !stuck) // Eğer Kunai bir duvara çarptıysa
        {
            StickToWall(other);
        }
    }

    private void StickToWall(Collider2D wall)
    {
        stuck = true;
        rb.linearVelocity = Vector2.zero; // Hareketi durdur
        rb.isKinematic = true; // Fizik etkilerini kapat
        transform.parent = wall.transform; // Kunai'yi duvarın çocuğu yap, böylece duvarla beraber hareket eder

        if (trail != null)
        {
            trail.emitting = false; // Saplandıktan sonra izi kes
        }
    }
}
