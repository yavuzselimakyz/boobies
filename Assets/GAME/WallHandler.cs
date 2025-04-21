using UnityEngine;

public class WallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("Jump Settings")]
    [SerializeField] private float wallSlidingSpeed = 5f;
    [SerializeField] private float wallJumpingTime = 0.2f;
    [SerializeField] private float wallJumpingDuration = 0.1f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(5f, 14f);

    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingCounter;
    private float wallJumpingDirection;

    public bool IsWallSliding => isWallSliding;

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    public void UpdateHandler(bool isGrounded, float horizontal)
    {
        if (IsWalled() && !isGrounded && horizontal != 0f)
        {
            isWallSliding = true;
            float slideSpeed = Input.GetKey(KeyCode.S) ? wallSlidingSpeed * 2f : wallSlidingSpeed;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -slideSpeed, float.MaxValue));
            animator.SetBool("isWallSliding", true);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("isWallSliding", false);
        }

        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            // Flip karakter
            if (transform.localScale.x != wallJumpingDirection)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            animator.SetBool("isWallSliding", false);
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    public bool IsWallJumping()
    {
        return isWallJumping;
    }
}
