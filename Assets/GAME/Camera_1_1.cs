using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera_1_1 : MonoBehaviour
{
    public Transform player;  // Takip edilecek karakter
    public List<Border> borders;  // Kamera kaymas�n� tetikleyecek s�n�rlar�n listesi
    public float smoothSpeed = 0.125f; // Kameran�n kayma yumu�akl���

    private int currentBorderIndex = 0; // Hangi s�n�rda oldu�umuzu takip ederiz

    void OnTriggerEnter2D(Collider2D collider)
    {
        // E�er player tag'l� obje ile trigger olay� ger�ekle�irse
        if (collider.tag == "Player"){
            if (currentBorderIndex < borders.Count) // E�er hala ge�ilecek s�n�r varsa
            {
                Border currentBorder = borders[currentBorderIndex];

                // E�er karakter s�n�r� ge�tiyse, kameray� kayd�r
                if (player.position.x > currentBorder.borderPosition.position.x || player.position.y > currentBorder.borderPosition.position.y)
                {
                    currentBorderIndex++; // Bir sonraki s�n�ra ge�
                    Vector3 targetPosition = transform.position;
                    targetPosition.x += currentBorder.cameraShift.x; // X ekseninde kayd�r
                    targetPosition.y += currentBorder.cameraShift.y; // Y ekseninde kayd�r

                    // Kameray� yumu�ak bir �ekilde yeni pozisyona ge�ir
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
    public Transform borderPosition;  // S�n�r objesinin Transform'u
    public Vector2 cameraShift;       // S�n�r ge�ildi�inde kameran�n kayaca�� mesafe (X, Y)
}
