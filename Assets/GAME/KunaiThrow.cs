using UnityEngine;

public class KunaiThrow : MonoBehaviour
{
    public Movement playerMovementScript;
    public GameObject kunaiPrefab;
    public Transform throwPoint;
    public float kunaiSpeed = 10f;
    public float minInputThreshold = 0.2f;

    public bool isBulletTime = false; // Zaman yavaşlatma modu
    public float bulletTimeFactor = 0.2f; // Yavaşlatma oranı
    public float bulletTimeDuration = 3f; // Yavaşlatma süresi
    private float bulletTimeCounter;

    private bool isAiming = false;
    private Vector2 currentAimDirection = Vector2.right; // varsay�lan y�n

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EnterAimMode();
        }

        if (Input.GetKey(KeyCode.F))
        {
            UpdateAimDirection();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            if (isAiming && currentAimDirection.magnitude >= minInputThreshold)
            {
                ThrowKunai(currentAimDirection.normalized);
            }
            ExitAimMode();
        }
        if (isBulletTime)
        {
            // Bullet Time'da zamanı yavaşlat
            Time.timeScale = bulletTimeFactor;
            bulletTimeCounter -= Time.unscaledDeltaTime;

            if (bulletTimeCounter <= 0f)
            {
                ExitBulletTime(); // Süre dolduğunda Bullet Time'dan çık
            }
        }
        else
        {
            Time.timeScale = 1f; // Zaman normal hızda
        }
    }

    void EnterAimMode()
    {
        isAiming = true;
        currentAimDirection = Vector2.right;

        // Karakter hareketini durdur
        if (playerMovementScript != null)
        {
            playerMovementScript.canMove = false;

            Rigidbody2D rb = playerMovementScript.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; // hemen dur
            }

            Animator animator = playerMovementScript.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetFloat("Speed", 0f); // koşu animasyonunu kapat
            }
        }

        Debug.Log("Aim Mode ON");
    }

    void ExitAimMode()
    {
        isAiming = false;
        playerMovementScript.canMove = true; // hareketi geri aç
        Debug.Log("Aim Mode Off");
    }

    void UpdateAimDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(x, y);
        if (input.magnitude > minInputThreshold)
        {
            currentAimDirection = input.normalized;
        }
    }


    void ThrowKunai(Vector2 direction)
    {
        GameObject kunai = Instantiate(kunaiPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = kunai.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * kunaiSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        kunai.transform.rotation = Quaternion.Euler(0, 0, angle);

        Debug.Log("Kunai F�rlat�ld�: " + direction);
    }

    void EnterBulletTime()
    {
        isBulletTime = true;
        bulletTimeCounter = bulletTimeDuration; // Zaman sayacı başlasın
                                                // Ekstra efektler (mesela bir ses veya animasyon ekleyebilirsin)
    }

    void ExitBulletTime()
    {
        isBulletTime = false;
        Time.timeScale = 1f; // Zamanı normale döndür
                             // Efektleri sonlandır
    }
}