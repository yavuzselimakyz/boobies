using UnityEngine;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    public List<GameObject> boundaryObjects;  // Birden fazla sýnýr objesi için liste
    public float smoothSpeed = 0.125f;  // Kameranýn geçiþ hýzýný kontrol etmek için
    public float offsetX = 20f;  // Kameranýn X yönünde geçiþ miktarý
    public float offsetY = 15f;  // Kameranýn Y yönünde geçiþ miktarý
    public float minX = -10f;  // Kameranýn ulaþabileceði minimum X deðeri
    
    public float minY = -5f;   // Kameranýn ulaþabileceði minimum Y deðeri
    public float maxY = 50f;   // Kameranýn ulaþabileceði maksimum Y deðeri

    private Transform player;  // Player objesinin transform'u
    private int currentBoundaryIndexX = 0;  // X yönü için hangi boundary'nin geçildiðini takip eder
    private int currentBoundaryIndexY = 0;  // Y yönü için hangi boundary'nin geçildiðini takip eder
    private Vector3 targetPosition;  // Kameranýn gitmesi gereken hedef pozisyonu

    void Start()
    {
        targetPosition = transform.position;
        boundaryObjects.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x)); // X'e göre sýrala
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // --- X EKSENÝNDE KONTROL ---
        if (currentBoundaryIndexX < boundaryObjects.Count)
        {
            float boundaryX = boundaryObjects[currentBoundaryIndexX].transform.position.x;

            if (player.position.x > boundaryX)
            {
                targetPosition.x += offsetX;  // Kamerayý saða kaydýr
                currentBoundaryIndexX++;
            }
        }

        if (currentBoundaryIndexX > 0 && player.position.x < boundaryObjects[currentBoundaryIndexX - 1].transform.position.x)
        {
            targetPosition.x -= offsetX;
            currentBoundaryIndexX--;
        }

        // --- Y EKSENÝNDE KONTROL ---
        if (currentBoundaryIndexY < boundaryObjects.Count)
        {
            float boundaryY = boundaryObjects[currentBoundaryIndexY].transform.position.y;

            if (player.position.y > boundaryY)
            {
                targetPosition.y += offsetY;  // Kamerayý yukarý kaydýr
                currentBoundaryIndexY++;
            }
        }

        if (currentBoundaryIndexY > 0 && player.position.y < boundaryObjects[currentBoundaryIndexY - 1].transform.position.y)
        {
            targetPosition.y -= offsetY;
            currentBoundaryIndexY--;
        }

        // Sýnýrlarý uygula
        targetPosition.x = Mathf.Max(targetPosition.x, minX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // Kamerayý yumuþakça hedef pozisyona taþý
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}