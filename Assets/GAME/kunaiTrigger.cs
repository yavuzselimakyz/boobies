using UnityEngine;

public class kunaiTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlatformMove PlatformMove;
    private void Start()
    {
        
        PlatformMove = GetComponent<PlatformMove>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Kunai")
            {
                PlatformMove.moving = true;
            }
        }
    }
 
}
