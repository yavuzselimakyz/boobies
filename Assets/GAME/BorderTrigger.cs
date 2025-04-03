using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BorderTrigger : MonoBehaviour
{
    public Vector3 targetCameraPosition; // Kameranýn gitmesi gereken konum
    public float smoothSpeed = 0.125f; // Yumuþak geçiþ hýzý

    private Camera mainCamera;
    private static Stack<Vector3> cameraHistory = new Stack<Vector3>(); // Kamera geçmiþini tutan yýðýn (LIFO)
    private bool isMoving = false; // Geçiþin devam edip etmediðini kontrol eden flag

    void Start()
    {
        mainCamera = Camera.main; // Ana kamerayý al
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) // Eðer Player ile trigger olursa
        {
            // Eðer hareket etmiyorsa, geçiþ baþlat
            if (!isMoving)
            {
                Vector3 currentCameraPosition = mainCamera.transform.position;

                if (cameraHistory.Count > 0 && cameraHistory.Peek() == targetCameraPosition)
                {
                    // Eðer ayný konuma gelirse geri gitme iþlemi
                    cameraHistory.Pop(); // Önceki pozisyonu çýkar
                    Debug.Log("Player geri döndü, kamera eski konuma gidiyor!");
                    StartCoroutine(SmoothMove(cameraHistory.Peek(), true)); // Geri hareket et
                }
                else
                {
                    // Yeni pozisyona gitmek için kaydet
                    cameraHistory.Push(currentCameraPosition);
                    Debug.Log("Player ilerledi, kamera yeni konuma gidiyor!");
                    StartCoroutine(SmoothMove(targetCameraPosition, false)); // Ýleri hareket et
                }
            }
        }
    }

    IEnumerator SmoothMove(Vector3 targetPosition, bool isGoingBack)
    {
        isMoving = true; // Hareket baþlýyor

        // Hareket yönüne göre geçiþi yap
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, smoothSpeed);
            yield return null;
        }

        mainCamera.transform.position = targetPosition; // Son konuma tam olarak oturt

        isMoving = false; // Hareket bitti
    }
}
