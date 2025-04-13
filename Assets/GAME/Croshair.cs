using Unity.VisualScripting;
using UnityEngine;

public class Croshair : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    kunaistate kunai;
    private void Start()
    {
        kunaistate kunai =  gameObject.AddComponent<kunaistate>();
    }

    
    void Update()
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Z pozisyonunu sabitle (çünkü ScreenToWorldPoint Z'yi etkiler)
        mousePosition.z = 0f;

        // Objeyi mouse pozisyonuna taþý
        transform.position = mousePosition;
    }
}
