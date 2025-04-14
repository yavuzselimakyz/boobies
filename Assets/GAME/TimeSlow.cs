using UnityEngine;

public class TimeSlow : MonoBehaviour
{

    public float slowTimeScale = 0.2f;
    private float normalTimeScale = 1f;

    Movement move;
    KunaiThrower kunaiThrower;
    private void Start()
    {
        kunaiThrower = GetComponent<KunaiThrower>();
        move = GetComponent<Movement>();

    }
    void Update()
    {

        if (Input.GetMouseButton(1)) // Sa� t�k bas�l�ysa
        {
            Time.timeScale = slowTimeScale;
            move.canMove = true;
        }
        else
        {
            Time.timeScale = normalTimeScale;
            move.canMove = false;

        }

        // Zaman yava�lad���nda fiziksel olaylar�n d�zg�n �al��mas� i�in:
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    
}
