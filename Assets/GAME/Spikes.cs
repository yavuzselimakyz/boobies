using UnityEngine;

public class Spikes : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Sadece oyuncuya etki etsin
        {
            Destroy(collision.gameObject);            
        }
    }
}