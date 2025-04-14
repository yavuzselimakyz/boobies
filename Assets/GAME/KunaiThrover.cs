using UnityEngine;

public class KunaiThrower : MonoBehaviour
{
    public GameObject kunaiPrefab;         // Kunai prefab'ı
    public Transform kunaiSpawnPoint;      // Kunai'nin fırlatılacağı yer
    public GameObject crosshairPrefab;     // Crosshair prefab'ı (sahneye yerleştirilen prefab)
    public float throwForce = 20f;         // Fırlatma gücü

    private GameObject crosshairInstance;  // Crosshair'in instansiyonu
    private bool isAiming = false;         // Hedef alınıp alınmadığını kontrol eder

    void Start()
    {
        // Başlangıçta crosshair gizli olmalı
        crosshairInstance = Instantiate(crosshairPrefab);
        crosshairInstance.SetActive(false);  // Başlangıçta görünmesin
    }

    void Update()
    {
        // Sağ tıkla nişan aç
        if (Input.GetMouseButtonDown(1))
        {
            crosshairInstance.SetActive(true);  // Crosshair'i aktif et
            isAiming = true;
        }

        // Sağ tıkı bırakınca nişanı kapat
        if (Input.GetMouseButtonUp(1))
        {
            crosshairInstance.SetActive(false); // Crosshair'i gizle
            isAiming = false;
        }

        // Sol tıkla fırlat
        if (isAiming && Input.GetMouseButtonDown(0))
        {
            ThrowKunai();  // Kunai'yi fırlat
        }
    }

    void ThrowKunai()
    {
        // Crosshair'e doğru yön belirle
        Vector3 direction = (crosshairInstance.transform.position - kunaiSpawnPoint.position).normalized;

        // Kunai oluştur
        GameObject kunai = Instantiate(kunaiPrefab, kunaiSpawnPoint.position, Quaternion.identity);

        // Kunai'nin doğru yönü alması için rotasını crosshair'e göre döndür
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        kunai.transform.rotation = Quaternion.Euler(0, 0, angle);  // Yönü doğru ayarla

        // Kunai'nin fırlatılması için kuvvet uygula
        Rigidbody2D rb = kunai.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
    }
}
