using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BorderTrigger : MonoBehaviour
{
    public Vector3 targetCameraPosition; // Kameran�n gitmesi gereken konum
    public float smoothSpeed = 0.125f; // Yumu�ak ge�i� h�z�

    private Camera mainCamera;
    private static Stack<Vector3> cameraHistory = new Stack<Vector3>(); // Kamera ge�mi�ini tutan y���n (LIFO)
    private bool isMoving = false; // Ge�i�in devam edip etmedi�ini kontrol eden flag

    void Start()
    {
        mainCamera = Camera.main; // Ana kameray� al
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) // E�er Player ile trigger olursa
        {
            // E�er hareket etmiyorsa, ge�i� ba�lat
            if (!isMoving)
            {
                Vector3 currentCameraPosition = mainCamera.transform.position;

                if (cameraHistory.Count > 0 && cameraHistory.Peek() == targetCameraPosition)
                {
                    // E�er ayn� konuma gelirse geri gitme i�lemi
                    cameraHistory.Pop(); // �nceki pozisyonu ��kar
                    Debug.Log("Player geri d�nd�, kamera eski konuma gidiyor!");
                    StartCoroutine(SmoothMove(cameraHistory.Peek(), true)); // Geri hareket et
                }
                else
                {
                    // Yeni pozisyona gitmek i�in kaydet
                    cameraHistory.Push(currentCameraPosition);
                    Debug.Log("Player ilerledi, kamera yeni konuma gidiyor!");
                    StartCoroutine(SmoothMove(targetCameraPosition, false)); // �leri hareket et
                }
            }
        }
    }

    IEnumerator SmoothMove(Vector3 targetPosition, bool isGoingBack)
    {
        isMoving = true; // Hareket ba�l�yor

        // Hareket y�n�ne g�re ge�i�i yap
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, smoothSpeed);
            yield return null;
        }

        mainCamera.transform.position = targetPosition; // Son konuma tam olarak oturt

        isMoving = false; // Hareket bitti
    }
}
