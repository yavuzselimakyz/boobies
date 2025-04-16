using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    public float slowTimeScale = 0.2f;
    private float normalTimeScale = 1f;
    private bool isSlowed = false;

    private Movement move;
    private KunaiThrower kunaiThrower;
    private Rigidbody2D rb;
    [SerializeField] private string[] stopTags = { "Ground"};

    private void Start()
    {
        kunaiThrower = GetComponent<KunaiThrower>();
        move = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (!isSlowed)
            {
                Time.timeScale = slowTimeScale;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                move.canMove = false;
                isSlowed = true;
            }
        }
        else
        {
            if (isSlowed)
            {
                Time.timeScale = normalTimeScale;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                move.canMove = true;
                isSlowed = false;
            }
        }
    }


    void FixedUpdate()
    {
        if (isSlowed)
        {
            // X ekseni hareketi komple durduruluyor
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            // İstersen havada da durdursun:
            // rb.velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetMouseButton(1)) // SAĞ TIK BASILIYKEN
        {
            foreach (string tag in stopTags)
            {
                if (collision.gameObject.CompareTag(tag))
                {
                    StopMomentum(); // SADECE DURDUR
                    break;
                }
            }
        }
    }

    private void StopMomentum()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }


    private void RestoreMovement()
    {
        if (move != null)
        {
            move.canMove = true;
        }
    }
}
