using UnityEngine;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    public List<GameObject> boundaryObjects;  // Birden fazla s�n�r objesi i�in liste
    public float smoothSpeed = 0.125f;  // Kameran�n ge�i� h�z�n� kontrol etmek i�in
    public float offsetX = 20f;  // Kameran�n X y�n�nde ge�i� miktar�
    public float offsetY = 15f;  // Kameran�n Y y�n�nde ge�i� miktar�
    public float minX = -10f;  // Kameran�n ula�abilece�i minimum X de�eri
    
    public float minY = -5f;   // Kameran�n ula�abilece�i minimum Y de�eri
    public float maxY = 50f;   // Kameran�n ula�abilece�i maksimum Y de�eri

    private Transform player;  // Player objesinin transform'u
    private int currentBoundaryIndexX = 0;  // X y�n� i�in hangi boundary'nin ge�ildi�ini takip eder
    private int currentBoundaryIndexY = 0;  // Y y�n� i�in hangi boundary'nin ge�ildi�ini takip eder
    private Vector3 targetPosition;  // Kameran�n gitmesi gereken hedef pozisyonu

    void Start()
    {
        targetPosition = transform.position;
        boundaryObjects.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x)); // X'e g�re s�rala
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // --- X EKSEN�NDE KONTROL ---
        if (currentBoundaryIndexX < boundaryObjects.Count)
        {
            float boundaryX = boundaryObjects[currentBoundaryIndexX].transform.position.x;

            if (player.position.x > boundaryX)
            {
                targetPosition.x += offsetX;  // Kameray� sa�a kayd�r
                currentBoundaryIndexX++;
            }
        }

        if (currentBoundaryIndexX > 0 && player.position.x < boundaryObjects[currentBoundaryIndexX - 1].transform.position.x)
        {
            targetPosition.x -= offsetX;
            currentBoundaryIndexX--;
        }

        // --- Y EKSEN�NDE KONTROL ---
        if (currentBoundaryIndexY < boundaryObjects.Count)
        {
            float boundaryY = boundaryObjects[currentBoundaryIndexY].transform.position.y;

            if (player.position.y > boundaryY)
            {
                targetPosition.y += offsetY;  // Kameray� yukar� kayd�r
                currentBoundaryIndexY++;
            }
        }

        if (currentBoundaryIndexY > 0 && player.position.y < boundaryObjects[currentBoundaryIndexY - 1].transform.position.y)
        {
            targetPosition.y -= offsetY;
            currentBoundaryIndexY--;
        }

        // S�n�rlar� uygula
        targetPosition.x = Mathf.Max(targetPosition.x, minX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // Kameray� yumu�ak�a hedef pozisyona ta��
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}