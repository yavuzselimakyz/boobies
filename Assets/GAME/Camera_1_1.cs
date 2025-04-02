using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera_1_1 : MonoBehaviour
{
    public Transform player;  // Takip edilecek karakter
    public List<Border> borders;  // Kamera kaymasýný tetikleyecek sýnýrlarýn listesi
    public float smoothSpeed = 0.125f; // Kameranýn kayma yumuþaklýðý

    private int currentBorderIndex = 0; // Hangi sýnýrda olduðumuzu takip ederiz

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Eðer player tag'lý obje ile trigger olayý gerçekleþirse
        if (collider.tag == "Player"){
            if (currentBorderIndex < borders.Count) // Eðer hala geçilecek sýnýr varsa
            {
                Border currentBorder = borders[currentBorderIndex];

                // Eðer karakter sýnýrý geçtiyse, kamerayý kaydýr
                if (player.position.x > currentBorder.borderPosition.position.x || player.position.y > currentBorder.borderPosition.position.y)
                {
                    currentBorderIndex++; // Bir sonraki sýnýra geç
                    Vector3 targetPosition = transform.position;
                    targetPosition.x += currentBorder.cameraShift.x; // X ekseninde kaydýr
                    targetPosition.y += currentBorder.cameraShift.y; // Y ekseninde kaydýr

                    // Kamerayý yumuþak bir þekilde yeni pozisyona geçir
                    StartCoroutine(SmoothMove(targetPosition));
                }
            }
        }
    }

    IEnumerator SmoothMove(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            yield return null;
        }
    }
}

[System.Serializable]
public class Border
{
    public Transform borderPosition;  // Sýnýr objesinin Transform'u
    public Vector2 cameraShift;       // Sýnýr geçildiðinde kameranýn kayacaðý mesafe (X, Y)
}
