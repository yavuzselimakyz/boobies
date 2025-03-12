using UnityEngine;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    public List<GameObject> boundaryObjects;  // Birden fazla sýnýr objesi için liste
    public float smoothSpeed = 0.125f;  // Kameranýn geçiþ hýzýný kontrol etmek için
    public float offset = 20f;  // Kameranýn geçiþ miktarý (+20 veya -20)
    public float minX = -10f;  // Kameranýn ulaþabileceði minimum X deðeri

    private Transform player;  // Player objesinin transform'u
    private int currentBoundaryIndex = 0;  // Hangi boundary'nin geçildiðini takip eder
    private Vector3 targetPosition;  // Kameranýn gitmesi gereken hedef pozisyonu

    void Start()
    {
        targetPosition = transform.position;

        // Boundary objelerini X sýrasýna göre sýrala
        boundaryObjects.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Eðer geçilecek bir sýnýr varsa
        if (currentBoundaryIndex < boundaryObjects.Count)
        {
            float boundaryX = boundaryObjects[currentBoundaryIndex].transform.position.x;

            // Eðer oyuncu mevcut boundary'yi geçerse, kamera kaydýrýlýr
            if (player.position.x > boundaryX)
            {
                targetPosition.x += offset;  // Kamerayý saða kaydýr
                currentBoundaryIndex++;  // Bir sonraki boundary'ye geç
            }
        }

        // Eðer oyuncu bir sýnýrdan geri geçmeye baþlarsa
        if (currentBoundaryIndex > 0 && player.position.x < boundaryObjects[currentBoundaryIndex - 1].transform.position.x)
        {
            // Kamerayý sola kaydýr, bir önceki sýnýrdan geriye doðru
            targetPosition.x -= offset;
            currentBoundaryIndex--;  // Bir önceki boundary'ye geri dön
        }

        // Minimum X sýnýrýný uygula
        targetPosition.x = Mathf.Max(targetPosition.x, minX);

        // Kamerayý yavaþça hedef pozisyona hareket ettir
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
