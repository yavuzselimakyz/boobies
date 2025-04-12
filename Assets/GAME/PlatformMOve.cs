using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform target; // Hedef obje
    public float speed = 2f; // Platform hýzý
    public bool moving = false; // Hareket kontrolü

     void Update()
    {
        if (moving && target != null)
        {
            // Hedefe doðru hareket et
            if (moving == true) 
            {

                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                // Hedefe ulaþtý mý?
                if (Vector2.Distance(transform.position, target.position) < 0.1f)
                {
                    moving = false; // Hareketi durdur
                }
            }

        }
    }
}
