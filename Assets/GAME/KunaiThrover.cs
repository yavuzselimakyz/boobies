using UnityEngine;

public class KunaiThrower : MonoBehaviour
{
    public GameObject kunaiPrefab;
    public Transform kunaiSpawnPoint;
    public GameObject crosshairPrefab;
    public float throwForce = 20f;
    public float throwCooldown = 1f; // 1 saniye bekleme süresi

    private GameObject crosshairInstance;
    private bool isAiming = false;
    private float lastThrowTime = -Mathf.Infinity; // Başlangıçta fırlatmaya izin ver

    void Start()
    {
        crosshairInstance = Instantiate(crosshairPrefab);
        crosshairInstance.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            crosshairInstance.SetActive(true);
            isAiming = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            crosshairInstance.SetActive(false);
            isAiming = false;
        }

        if (isAiming && Input.GetMouseButtonDown(0))
        {
            // Spam koruması: sadece belirli aralıkla atılabilir
            if (Time.time >= lastThrowTime + throwCooldown)
            {
                ThrowKunai();
                lastThrowTime = Time.time;
            }
        }
    }

    void ThrowKunai()
    {
        Vector3 direction = (crosshairInstance.transform.position - kunaiSpawnPoint.position).normalized;

        GameObject kunai = Instantiate(kunaiPrefab, kunaiSpawnPoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        kunai.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rb = kunai.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
    }
}
