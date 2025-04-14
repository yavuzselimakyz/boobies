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

        if (Input.GetMouseButton(1)) // Sað týk basýlýysa
        {
            Time.timeScale = slowTimeScale;
            move.canMove = true;
        }
        else
        {
            Time.timeScale = normalTimeScale;
            move.canMove = false;

        }

        // Zaman yavaþladýðýnda fiziksel olaylarýn düzgün çalýþmasý için:
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    
}
