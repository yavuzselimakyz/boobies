using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CameraControl : MonoBehaviour
{
   
    void Start()
    {
        //targetPosition = transform.position;
        //boundaryObjects.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x)); // X'e g�re s�rala
    }

    void Update()
    {


        //    if (currentBoundaryIndexY > 0 && player.position.y < boundaryObjects[currentBoundaryIndexY - 1].transform.position.y)
        //    {
        //        targetPosition.y -= offsetY;
        //        currentBoundaryIndexY--;

        //    }
        //    return;

        //    // --- X EKSEN�NDE KONTROL ---
        //    if (currentBoundaryIndexX < boundaryObjects.Count)
        //    {
        //        float boundaryX = boundaryObjects[currentBoundaryIndexX].transform.position.x;

        //        if (player.position.x > boundaryX)
        //        {
        //            targetPosition.x += offsetX;  // Kameray� sa�a kayd�r
        //            currentBoundaryIndexX++;
        //        }
        //    }

        //    if (currentBoundaryIndexX > 0 && player.position.x < boundaryObjects[currentBoundaryIndexX - 1].transform.position.x)
        //    {
        //        targetPosition.x -= offsetX;
        //        currentBoundaryIndexX--;
        //    }


        //    // --- Y EKSEN�NDE KONTROL ---
        //    if (currentBoundaryIndexY < boundaryObjects.Count)
        //    {
        //        float boundaryY = boundaryObjects[currentBoundaryIndexY].transform.position.y;

        //        if (player.position.y > boundaryY)
        //        {
        //            targetPosition.y += offsetY;  // Kameray� yukar� kayd�r
        //            currentBoundaryIndexY++;
        //        }
        //    }

        //    if (currentBoundaryIndexY > 0 && player.position.y < boundaryObjects[currentBoundaryIndexY - 1].transform.position.y)
        //    {
        //        targetPosition.y -= offsetY;
        //        currentBoundaryIndexY--;
        //    }

        //    // S�n�rlar� uygula
        //    targetPosition.x = Mathf.Max(targetPosition.x, minX);
        //    targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        //    // Kameray� yumu�ak�a hedef pozisyona ta��
        //    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        //}

    }
}