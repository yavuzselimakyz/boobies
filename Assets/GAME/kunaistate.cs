using System.Collections;
using UnityEngine;

public class kunaistate : MonoBehaviour
{
    private bool isKunaiMode = false;
    private float slowMotionTimeScale = 0.2f;
    private float normalTimeScale = 1f;
    private float slowMotionDuration = 1f;
    private Coroutine slowMotionCoroutine;

    private Movement characterMovement;
    public GameObject kunaiPrefab;
    public float throwForce = 20f;
    private Vector2 throwDirection;
    public Transform kunaiSpawnPoint;

    private void Start()
    {
        characterMovement = GetComponent<Movement>();
        if (kunaiSpawnPoint == null)
        {
            Debug.LogError("Kunai Spawn Point atanmamış!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            EnterKunaiMode();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            ExitKunaiMode();
            ThrowKunai();
        }

        if (isKunaiMode)
        {
            HandleKunaiDirection();
        }
    }

    private void EnterKunaiMode()
    {
        isKunaiMode = true;
        Time.timeScale = slowMotionTimeScale;
        slowMotionCoroutine = StartCoroutine(ResetTimeScaleAfterDelay());

        if (characterMovement != null)
        {
            characterMovement.enabled = false;
        }
    }

    private void ExitKunaiMode()
    {
        isKunaiMode = false;

        if (slowMotionCoroutine != null)
        {
            StopCoroutine(slowMotionCoroutine);
        }

        Time.timeScale = normalTimeScale;

        if (characterMovement != null)
        {
            characterMovement.enabled = true;
        }
    }

    private void HandleKunaiDirection()
    {
        float x = 0, y = 0;

        if (Input.GetKey(KeyCode.UpArrow)) y = 1;
        if (Input.GetKey(KeyCode.DownArrow)) y = -1;
        if (Input.GetKey(KeyCode.LeftArrow)) x = -1;
        if (Input.GetKey(KeyCode.RightArrow)) x = 1;

        throwDirection = new Vector2(x, y).normalized; // Çapraz yönleri desteklemek için normalize ettik
    }

    private void ThrowKunai()
    {
        if (throwDirection == Vector2.zero)
        {
            throwDirection = new Vector2(transform.localScale.x, 0); // Varsayılan olarak karakterin yönüne fırlat
        }

        GameObject kunai = Instantiate(kunaiPrefab, kunaiSpawnPoint.position, Quaternion.identity);
        Rigidbody2D kunaiRb = kunai.GetComponent<Rigidbody2D>();

        if (kunaiRb != null)
        {
            kunaiRb.linearVelocity = throwDirection * throwForce;
        }
        else
        {
            Debug.LogError("Kunai Rigidbody2D component'ı eksik!");
        }
    }

    private IEnumerator ResetTimeScaleAfterDelay()
    {
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        Time.timeScale = normalTimeScale;
    }
}
