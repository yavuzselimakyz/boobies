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
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Trigger Çalýþtý! Player: " + collider.name);
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
