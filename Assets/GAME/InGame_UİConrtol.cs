using UnityEngine;

public class InGameUIControl : MonoBehaviour
{
    private GameObject currentPanel; // O an aktif olan panel

    private void Start()
    {
        // Sahnedeki tüm UiTrigger bileþenlerini bul ve panellerini kapat
        UiTrigger[] triggers = FindObjectsOfType<UiTrigger>();

        foreach (UiTrigger trigger in triggers)
        {
            if (trigger.linkedPanel != null)
            {
                trigger.linkedPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        UiTrigger panelTrigger = other.GetComponent<UiTrigger>();

        if (panelTrigger != null)
        {
            if (currentPanel != null) // Eðer baþka bir panel açýksa kapat
            {
                currentPanel.SetActive(false);
            }

            currentPanel = panelTrigger.linkedPanel; // Yeni paneli ata
            currentPanel.SetActive(true); // Paneli aç
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UiTrigger panelTrigger = other.GetComponent<UiTrigger>();

        if (panelTrigger != null && currentPanel == panelTrigger.linkedPanel)
        {
            currentPanel.SetActive(false); // Paneli kapat
            currentPanel = null;
        }
    }
}
