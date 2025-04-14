using UnityEngine;

public class KunaiThrower : MonoBehaviour
{
    public GameObject kunaiPrefab;
    public Transform kunaiSpawnPoint;
    public GameObject crosshairPrefab;
    public float throwForce = 20f;
    public float throwCooldown = 1f;

    private GameObject crosshairInstance;
    private bool isAiming = false;
    private float lastThrowTime = -Mathf.Infinity;

    private byte kunaiAmount = 3;
    
    void Start()
    {
        crosshairInstance = Instantiate(crosshairPrefab);
        crosshairInstance.SetActive(false);
        
    }

    void Update()
    {
        if (kunaiAmount <= 0)
        {
            crosshairInstance.SetActive(false);
            return;
        }

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
            if (Time.time >= lastThrowTime + throwCooldown)
            {
                ThrowKunai();
                kunaiAmount--;
                
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

    // 🔼 Dışarıdan kunai sayısını artırmak için
    public void AddKunai(byte amount = 1)
    {
        kunaiAmount += amount;
    }

    // 🔽 Dilersen bu methodu da daha sade hale getirebiliriz
    public byte GetKunaiAmount()
    {
        return kunaiAmount;
    }
}
