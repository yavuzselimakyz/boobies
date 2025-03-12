using UnityEngine;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    public List<GameObject> boundaryObjects;  // Birden fazla s�n�r objesi i�in liste
    public float smoothSpeed = 0.125f;  // Kameran�n ge�i� h�z�n� kontrol etmek i�in
    public float offset = 20f;  // Kameran�n ge�i� miktar� (+20 veya -20)
    public float minX = -10f;  // Kameran�n ula�abilece�i minimum X de�eri

    private Transform player;  // Player objesinin transform'u
    private int currentBoundaryIndex = 0;  // Hangi boundary'nin ge�ildi�ini takip eder
    private Vector3 targetPosition;  // Kameran�n gitmesi gereken hedef pozisyonu

    void Start()
    {
        targetPosition = transform.position;

        // Boundary objelerini X s�ras�na g�re s�rala
        boundaryObjects.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // E�er ge�ilecek bir s�n�r varsa
        if (currentBoundaryIndex < boundaryObjects.Count)
        {
            float boundaryX = boundaryObjects[currentBoundaryIndex].transform.position.x;

            // E�er oyuncu mevcut boundary'yi ge�erse, kamera kayd�r�l�r
            if (player.position.x > boundaryX)
            {
                targetPosition.x += offset;  // Kameray� sa�a kayd�r
                currentBoundaryIndex++;  // Bir sonraki boundary'ye ge�
            }
        }

        // E�er oyuncu bir s�n�rdan geri ge�meye ba�larsa
        if (currentBoundaryIndex > 0 && player.position.x < boundaryObjects[currentBoundaryIndex - 1].transform.position.x)
        {
            // Kameray� sola kayd�r, bir �nceki s�n�rdan geriye do�ru
            targetPosition.x -= offset;
            currentBoundaryIndex--;  // Bir �nceki boundary'ye geri d�n
        }

        // Minimum X s�n�r�n� uygula
        targetPosition.x = Mathf.Max(targetPosition.x, minX);

        // Kameray� yava��a hedef pozisyona hareket ettir
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
