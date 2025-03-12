using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject playerPrefab; // Spawn edilecek Player prefabı
    public Transform spawnPoint; // Player'ın doğacağı konum
    private bool isRespawning = false; // Tekrar tekrar spawn olmaması için kontrol

    private void Update()
    {
        // Eğer sahnede "Player" tag'li obje yok ve respawn süreci başlamadıysa
        if (GameObject.FindGameObjectWithTag("Player") == null && !isRespawning)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    IEnumerator RespawnPlayer()
    {
        isRespawning = true; // Respawn sürecini başlat
        yield return new WaitForSeconds(1f); // 1 saniye bekle

        // Spawn edilen yeni player objesini oluştur
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        // Yeni oyuncuyu konsola yazdır
        Debug.Log("Yeni oyuncu spawn edildi: " + newPlayer.name);

        // Renderer bileşeninin aktif olup olmadığını kontrol et
        SpriteRenderer playerRenderer = newPlayer.GetComponent<SpriteRenderer>(); // Eğer 2D ise
        if (playerRenderer != null)
        {
            playerRenderer.enabled = true; // Renderer'ı aktif et
        }
        else
        {
            Debug.LogWarning("SpriteRenderer bulunamadı!");
        }

        // Eğer bir Rigidbody2D kullanıyorsa, onu kontrol et
        Rigidbody2D rb = newPlayer.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Hız sıfırlanabilir
            rb.isKinematic = false; // Kinematik değilse fizik etkileşimi olacak
        }

        isRespawning = false; // Respawn süreci tamamlandı
    }
}
