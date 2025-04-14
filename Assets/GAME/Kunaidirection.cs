using System.Collections.Generic;
using UnityEngine;

public class Kunaidirection : MonoBehaviour
{
    public Transform crosshair;           // Crosshair Transform'u
    public float stickDepth = 0.1f;       // Saplanma derinliği
    public List<string> targetTags;       // Etkileşime giren tagler
    private bool stuck = false;           // Saplanma kontrolü

    void Update()
    {
        // Buradaki kısım, sadece yönlendirmeyi kontrol etmek için
        // Kunai'nin rotası zaten `KunaiThrower.cs` tarafından doğru şekilde ayarlanacak
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (stuck) return;

        if (targetTags.Contains(other.tag))
        {
            stuck = true;
            transform.position += transform.right * stickDepth;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
            rb.simulated = false;
        }
    }
}
