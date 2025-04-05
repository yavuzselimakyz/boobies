using System.Collections;
using UnityEngine;

public class kunaistate : MonoBehaviour
{
    [SerializeField] private GameObject trajectoryDotPrefab;
    [SerializeField] private int numberOfDots = 20;
    [SerializeField] private float dotSpacing = 0.1f;
    [SerializeField] private float gravity = 9.8f;

    private GameObject[] trajectoryDots;

    [SerializeField] private GameObject kunaiPrefab;
    [SerializeField] private Transform kunaiSpawnPoint;
    [SerializeField] private float throwSpeed = 15f;
    [SerializeField] private float slowTimeScale = 0.2f;
    [SerializeField] private float throwCooldown = 0.5f;
    

    private Vector2 aimDirection = Vector2.right;
    private bool canThrow = true;

    [SerializeField] private Movement move;

    void Start()
    {
        
        trajectoryDots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            trajectoryDots[i] = Instantiate(trajectoryDotPrefab, transform.position, Quaternion.identity);
            trajectoryDots[i].SetActive(false);
        }
    }

    void Update()
    {
        if (!canThrow) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            move.canMove = false;    
            Time.timeScale = slowTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            
        }

        if (Input.GetKey(KeyCode.X))
        {
            move.canMove = false;

            UpdateAimDirection();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;

            
            
            HideTrajectory();

            ThrowKunai();
        }
    }

    void UpdateAimDirection()
    {
        float x = Input.GetKey(KeyCode.D) ? 1f : Input.GetKey(KeyCode.A) ? -1f : 0f;
        float y = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f;

        Vector2 dir = new Vector2(x, y);

        if (dir == Vector2.down || dir == Vector2.up) return;



        if (dir.sqrMagnitude > 0.1f)
        {
            aimDirection = dir.normalized;

            // Oku yönüne döndür
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            
        }
        if (dir != Vector2.zero)
        {
            ShowTrajectory();
        }
    }
    void ShowTrajectory()
    {
        Vector2 startPos = kunaiSpawnPoint.position;
        Vector2 startVelocity = aimDirection * throwSpeed;
        Vector2 gravityEffect = new Vector2(0, -gravity);

        for (int i = 0; i < numberOfDots; i++)
        {
            float t = i * dotSpacing;
            Vector2 pos = startPos + startVelocity * t + 0.5f * gravityEffect * t * t;

            trajectoryDots[i].transform.position = pos;
            trajectoryDots[i].SetActive(true);
        }
    }
    void HideTrajectory()
    {
        foreach (var dot in trajectoryDots)
        {
            dot.SetActive(false);
        }
    }
    void ThrowKunai()
    {
        canThrow = false;

        GameObject kunai = Instantiate(kunaiPrefab, kunaiSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = kunai.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearVelocity = aimDirection * throwSpeed;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    void ResetThrow()
    {
        canThrow = true;
        move.canMove = true;
    }
}


