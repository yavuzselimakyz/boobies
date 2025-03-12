using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f; // Zemin dokunulduktan kaç saniye sonra düşecek
    public float destroyDelay = 2f; // Zemin düştükten sonra kaç saniye içinde yok olacak
    public float shakeAmount = 0.1f; // Titreme şiddeti
    public float shakeDuration = 0.3f; // Titreme süresi

    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool isFalling = false;

    private Vector3 startPosition;
    private Collider2D platformCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformCollider = GetComponent<Collider2D>();
        rb.isKinematic = true; // Başlangıçta düşmemesi için
        originalPosition = transform.position; // Orijinal pozisyonu kaydet
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;
            StartCoroutine(ShakeAndDrop()); // Titreme ve düşme sürecini başlat
        }
    }

    void Update()
    {
        // Check if there's no object with the "Player" tag in the scene
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            comeback();
        }
    }

    IEnumerator ShakeAndDrop()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-shakeAmount, shakeAmount); // Sağa-sola rastgele kaydırma
            transform.position = originalPosition + new Vector3(xOffset, 0, 0);
            elapsed += Time.deltaTime;
            yield return null; // Bir sonraki kareyi bekle
        }

        transform.position = originalPosition; // Orijinal pozisyona dön
        yield return new WaitForSeconds(fallDelay - shakeDuration); // Titreme süresi bittikten sonra düşme süresi kadar bekle
        DropPlatform();
    }

    void DropPlatform()
    {
        rb.isKinematic = false; // Fizik etkisi aktif
        rb.gravityScale = 1; // Yerçekimi uygulanıyor
        StartCoroutine(DisableColliderAfterDelay(2f)); // 2 saniye sonra collider'ı kapat
    }

    IEnumerator DisableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        platformCollider.enabled = false; // Collider'ı kapat
    }

    void comeback()
    {
        StartCoroutine(SmoothReturnToStartPosition());
    }

    IEnumerator SmoothReturnToStartPosition()
    {
        float elapsed = 0f;
        float smoothTime = 1f; // Dönüş süresi (ihtiyaca göre ayarlanabilir)

        Vector3 startingPosition = transform.position;

        // Yavaşça geri dönme işlemi
        while (elapsed < smoothTime)
        {
            transform.position = Vector3.Lerp(startingPosition, startPosition, elapsed / smoothTime);
            elapsed += Time.deltaTime;
            yield return null; // Bir sonraki kareyi bekle
        }

        // Tamamen orijinal pozisyona ulaşsın
        transform.position = startPosition;

        // Fiziksel özellikleri resetle
        rb.isKinematic = true; // Kinematik yap
        rb.gravityScale = 0;   // Yerçekimini sıfırla

        // Falling state sıfırla
        isFalling = false;

        // Hızını sıfırla (opsiyonel)
        rb.linearVelocity = Vector2.zero;

        // Dönüş açısını sıfırla (opsiyonel)
        rb.rotation = 0;

        platformCollider.enabled = true; // Geri döndüğünde collider'ı tekrar aç
    }
}