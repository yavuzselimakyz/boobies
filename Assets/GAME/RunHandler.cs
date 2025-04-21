using UnityEngine;

public class RunHandler : MonoBehaviour
{
    [Header("Run Settings")]
    [SerializeField] private float runMaxSpeed = 1f;
    [SerializeField] private float runAccelAmount = 1f;
    [SerializeField] private float runDeccelAmount = 1f;
    [SerializeField] private float accelInAir = 0.5f;
    [SerializeField] private float deccelInAir = 0.5f;
    [SerializeField] private bool doConserveMomentum = true;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    private float horizontal;
    private float lastOnGroundTime;

    public void SetLastOnGroundTime(float time)
    {
        lastOnGroundTime = time;
    }

    public void UpdateHandler()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Animasyon kontrol�
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
    }

    public void FixedUpdateHandler()
    {
        float targetSpeed = horizontal * runMaxSpeed;
        float accelRate;

        if (lastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount * accelInAir : runDeccelAmount * deccelInAir;

        // Momentum koruma
        if (doConserveMomentum && Mathf.Abs(rb.linearVelocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.linearVelocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && lastOnGroundTime < 0)
        {
            accelRate = 0;
        }

        float speedDif = targetSpeed - rb.linearVelocity.x;
        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    public float GetHorizontal()
    {
        return horizontal;
    }
}
