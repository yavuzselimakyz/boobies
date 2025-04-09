using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{

    public GameObject playerPrefab;
    public Transform spawnPoint;
    private bool isRespawning = false;

    [Header("Kamera Ayarları")]
    public Transform cameraTransform; // Kamera objesi
    public Vector3 cameraResetPosition = new Vector3(17f, 5.5f, -10f); // Geri çekilme pozisyonu (Z sabit)

    public float cameraSmoothSpeed = 0.125f; // Kamera geçiş yumuşaklığı

    private void Update()
    {
        // Eğer sahnede "Player" tag'li obje yok ve respawn süreci başlamadıysa
        if (GameObject.FindGameObjectWithTag("Player") == null && !isRespawning)
        {
            StartCoroutine(RespawnPlayer());
        }
    }
    IEnumerator MoveCameraToPosition(Vector3 targetPos)
    {
        while (Vector3.Distance(cameraTransform.position, targetPos) > 0.01f)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPos, cameraSmoothSpeed);
            yield return null;
        }

        cameraTransform.position = targetPos; // Tam hedefe oturt
    }
    IEnumerator RespawnPlayer()
    {
        isRespawning = true;

        // Kamera hedef pozisyona yavaşça geçsin
        if (cameraTransform != null)
        {
            StartCoroutine(MoveCameraToPosition(cameraResetPosition));
        }

        yield return new WaitForSeconds(1f);

        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        Debug.Log("Yeni oyuncu spawn edildi: " + newPlayer.name);

        SpriteRenderer playerRenderer = newPlayer.GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            playerRenderer.enabled = true;
        }

        Rigidbody2D rb = newPlayer.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = false;
        }

        isRespawning = false;
    }
}
