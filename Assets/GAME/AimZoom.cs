using Unity.Cinemachine;
using UnityEngine;

public class AimZoom : MonoBehaviour
{
    public float normalZoom = 5f;
    public float aimZoom = 3.5f;
    public float zoomSpeed = 5f;

    private bool isAiming;
    KunaiThrower kunaiThrower;

    private void Start()
    {
        kunaiThrower = GetComponent<KunaiThrower>();
        
    }
    void Update()
    {
        if (kunaiThrower.GetKunaiAmount() <= 0 && kunaiThrower != null ) return;
        
        isAiming = Input.GetMouseButton(1);

        float targetZoom = isAiming ? aimZoom : normalZoom;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }
}

