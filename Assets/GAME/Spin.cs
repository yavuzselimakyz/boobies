using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 100f; // D�nd�rme h�z�

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}