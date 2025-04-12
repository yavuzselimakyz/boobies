using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform target; // Hedef obje
    public float speed = 2f; // Platform h�z�
    public bool moving = false; // Hareket kontrol�

     void Update()
    {
        if (moving && target != null)
        {
            // Hedefe do�ru hareket et
            if (moving == true) 
            {

                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                // Hedefe ula�t� m�?
                if (Vector2.Distance(transform.position, target.position) < 0.1f)
                {
                    moving = false; // Hareketi durdur
                }
            }

        }
    }
}
