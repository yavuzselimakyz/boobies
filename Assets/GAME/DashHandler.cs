using System.Collections;
using UnityEngine;

public class DashHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private GameObject afterimagePrefab;

    [Header("Dash Settings")]
    [SerializeField] private float dashingPower = 10f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float afterimageSpawnRate = 0.01f;
    [SerializeField] private float afterimageLifetime = 0.5f;

    private bool canDash = true;
    private bool isDashing = false;
    private float lastAfterimageTime;

    private bool isFacingRight = true;

    public bool IsDashing() => isDashing;

    public void SetFacingDirection(bool facingRight)
    {
        isFacingRight = facingRight;
    }

    public void TryDash(float horizontalInput, float verticalInput, System.Func<bool> isGrounded)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash(horizontalInput, verticalInput, isGrounded));
        }
    }

    private IEnumerator Dash(float horizontalInput, float verticalInput, System.Func<bool> isGrounded)
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("isDashing", true);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector2 dashDirection = new Vector2(horizontalInput, verticalInput).normalized;
        if (dashDirection == Vector2.zero)
        {
            dashDirection = isFacingRight ? Vector2.right : Vector2.left;
        }

        rb.linearVelocity = dashDirection * dashingPower;
        tr.emitting = true;

        float dashEndTime = Time.time + dashingTime;
        while (Time.time < dashEndTime)
        {
            CreateAfterimage();
            yield return new WaitForSeconds(afterimageSpawnRate);
        }

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", false);

        // Havadaysa yere inene kadar bekle
        if (!isGrounded())
        {
            yield return new WaitUntil(isGrounded);
        }

        canDash = true;
    }

    private void CreateAfterimage()
    {
        if (afterimagePrefab == null) return;

        GameObject afterimage = Instantiate(afterimagePrefab, transform.position, Quaternion.identity);
        SpriteRenderer afterimageRenderer = afterimage.GetComponent<SpriteRenderer>();
        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();

        if (afterimageRenderer != null && playerRenderer != null)
        {
            afterimageRenderer.sprite = playerRenderer.sprite;
            afterimageRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            afterimage.transform.localScale = transform.localScale;
        }

        Destroy(afterimage, afterimageLifetime);
    }

    public void ResetDash()
    {
        canDash = true;
    }
}
