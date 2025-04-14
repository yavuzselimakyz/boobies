using UnityEngine;

public class Croshair : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false; // Mouse'u gizle
        Cursor.lockState = CursorLockMode.Confined; // Mouse oyun ekranýndan çýkmasýn
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = mousePosition;
    }
}
