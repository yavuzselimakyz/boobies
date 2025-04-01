using UnityEngine;

public class InGameUIControl : MonoBehaviour
{
    private GameObject currentPanel; // O an aktif olan panel

    

    private void OnTriggerEnter2D(Collider2D other)
    {
         UiTrigger panelTrigger = other.GetComponent<UiTrigger>();

        if ( panelTrigger != null)
        {
            if (currentPanel != null) // E�er ba�ka bir panel a��ksa kapat
            {
                currentPanel.SetActive(false);
            }

            currentPanel = panelTrigger.linkedPanel; // Yeni paneli ata
            currentPanel.SetActive(true); // Paneli a�
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
