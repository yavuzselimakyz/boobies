using UnityEngine;

public class GetKunai : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject currentKunai;
    private KunaiThrower kunaiThrover;

    void Start()
    {
        kunaiThrover = GetComponent<KunaiThrower>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Kunai"))
        {
            currentKunai = other.gameObject;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(currentKunai);
                currentKunai = null;

                if (kunaiThrover != null)
                {
                    kunaiThrover.AddKunai(1);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Kunai"))
        {
            if (currentKunai == other.gameObject)
            {
                currentKunai = null;
            }
        }
    }
}
