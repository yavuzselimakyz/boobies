using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{


    // MORUK MOMENTUM GİBİ BİŞEY EKLEDİM AMK ?

    private float horizontal;
    private float speed = 1f;
    private float jumpingPower = 11f;
    private bool isFacingRight = true;
    public ParticleSystem dusteffct;

    private bool isWallSliding;
    private float wallSlidingSpeed = 5f;

    private float coyoteTime = 0.05f;
    private float coyoteTimeCounter;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.1f;
    private Vector2 wallJumpingPower = new Vector2(5f, 14f);

    private bool canDash = true;
    private bool isDashing;

    public bool canMove = true;
    private bool wasFalling = false;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.2f;

    [Header("Run Parameters")]
    public float runMaxSpeed = 1; // Maksimum hızS
    public float runAccelAmount = 1; // Hızlanma ivmesi
    public float runDeccelAmount = 1; // Yavaşlama ivmesi
    public float accelInAir = 0.5f; // Havada hızlanma çarpanı
    public float deccelInAir = 0.5f; // Havada yavaşlama çarpanı
    public bool doConserveMomentum = true; // Momentum korunacak mı?

    private float fadeDuration = 0.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] public GameObject afterimagePrefab;
    [SerializeField] private float afterimageSpawnRate = 0.01f;
    [SerializeField] private float afterimageLifetime = 0.5f;

    private float lastAfterimageTime;
    private float lastOnGroundTime; // En son yere değdiği zamanın süresi

    private void Update()
    {
        
        if (!canMove)
        {
            
            rb.linearVelocity = Vector2.zero;
            return; // input bile alma
        }



        if (isDashing)
        {
            return;
        }

        if (rb.linearVelocity.y < -0.1f)
        {
            wasFalling = true;
        }

        lastOnGroundTime -= Time.deltaTime; // Yere değme süresi güncelleniyor

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            lastOnGroundTime = 0.1f; // Yerde olduğunu kaydediyoruz
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0f)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f) // Zıplama tuşu değiştirildi
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            animator.SetBool("isRising", true);
            animator.SetBool("isFalling", false);
            coyoteTimeCounter = 0;
            createdust();
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f) // Zıplama tuşu değiştirildi
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }
    void ResetDash()
    {
        canDash = true;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            Run();
        }
    }

    private void Run()
    {
        float targetSpeed = horizontal * runMaxSpeed;
        float accelRate;

        if (lastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount * accelInAir : runDeccelAmount * deccelInAir;

        // Momentum koruma sistemi
        if (doConserveMomentum && Mathf.Abs(rb.linearVelocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.linearVelocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && lastOnGroundTime < 0)
        {
            accelRate = 0;
        }

        float speedDif = targetSpeed - rb.linearVelocity.x;
        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
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
    }
    void createdust()
    {

        if (dusteffct != null)
        {
            dusteffct.Play();
        }
        else
        {
            Debug.LogWarning("Dust Effect is missing!");
        }

    }
    private void WallJump()
    {
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

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f) // Zıplama tuşu değiştirildi
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;


            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            animator.SetBool("isWallSliding", false);
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike")) // Diken ile temas etti mi?
        {
            StartCoroutine(FadeAndDestroy()); // Kaybolma sürecini başlat
        }
        if (collision.gameObject.CompareTag("Ground") && wasFalling)
        {
            wasFalling = false;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dashreset"))
        {
            ResetDash();
            Destroy(other.gameObject);
        }
    }
    private void CreateAfterimage()
    {
        if (afterimagePrefab == null) return; // Eğer prefab yoksa çık

        GameObject afterimage = Instantiate(afterimagePrefab, transform.position, Quaternion.identity);
        SpriteRenderer afterimageRenderer = afterimage.GetComponent<SpriteRenderer>();
        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();

        if (afterimageRenderer != null && playerRenderer != null)
        {
            afterimageRenderer.sprite = playerRenderer.sprite; // Karakterin mevcut sprite'ını al
            afterimageRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Hafif transparan yap
            afterimage.transform.localScale = transform.localScale; // Karakterin ölçeğini kopyala
        }

        Destroy(afterimage, afterimageLifetime); // Belirlenen sürede sil
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("isDashing", true);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector2 dashDirection = new Vector2(horizontal, Input.GetAxisRaw("Vertical")).normalized;
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

        // Eğer karakter Dash sırasında yerdeyse, 2 saniye cooldown uygula
        if (IsGrounded())
        {
            yield return new WaitForSeconds(0f); // 2 saniye beklet
        }
        else
        {
            // Havadaysa, yere inene kadar bekle
            while (!IsGrounded())
            {
                yield return null;
            }
        }

        canDash = true;
    }
    IEnumerator FadeAndDestroy()
    {
        // Karakterin hareket girişlerini devre dışı bırak
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Hareketi sıfırla
        GetComponent<Rigidbody2D>().isKinematic = true; // Fizik hareketini devre dışı bırak

        SpriteRenderer sprite = GetComponent<SpriteRenderer>(); // Karakterin sprite'ını al
        Color color = sprite.color; // Mevcut rengi al
        float alpha = color.a; // Mevcut alpha değeri

        while (alpha > 0) // Tamamen şeffaf olana kadar
        {
            alpha -= Time.deltaTime / fadeDuration; // Alpha değerini azalt
            sprite.color = new Color(color.r, color.g, color.b, alpha); // Yeni rengi uygula
            yield return null; // Bir sonraki kareyi bekle
        }

        Destroy(gameObject); // Karakteri yok et
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}