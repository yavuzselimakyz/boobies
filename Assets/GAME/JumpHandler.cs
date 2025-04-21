using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpingPower = 11f;
    [SerializeField] private float coyoteTime = 0.05f;
    private float coyoteTimeCounter;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem dustEffect;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    private float lastOnGroundTime;

    public bool CanMove { get; set; } = true;

    public void UpdateHandler()
    {
        if (!CanMove) return;

        lastOnGroundTime -= Time.deltaTime;

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            lastOnGroundTime = 0.1f;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            animator.SetBool("isRising", true);
            animator.SetBool("isFalling", false);
            coyoteTimeCounter = 0;
            CreateDust();
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (rb.linearVelocity.y < 0f && !IsGrounded())
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isRising", false);
        }
        else if (rb.linearVelocity.y == 0f && IsGrounded())
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isRising", false);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void CreateDust()
    {
        if (dustEffect != null)
            dustEffect.Play();
        else
            Debug.LogWarning("Dust effect missing!");
    }

    public float GetLastOnGroundTime()
    {
        return lastOnGroundTime;
    }
}
