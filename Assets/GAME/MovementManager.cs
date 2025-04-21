using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private RunHandler runHandler;
    private JumpHandler jumpHandler;
    private DashHandler dashHandler;
    private WallHandler wallHandler;
    private FlipHandler flipHandler;
    private DeathHandler deathHandler;

    void Awake()
    {
        runHandler = GetComponent<RunHandler>();
        jumpHandler = GetComponent<JumpHandler>();
        dashHandler = GetComponent<DashHandler>();
        wallHandler = GetComponent<WallHandler>();
        flipHandler = GetComponent<FlipHandler>();
    }

    void Update()
    {
        
        if (!jumpHandler.CanMove) return;

        if (!wallHandler.IsWallJumping())
        {
            flipHandler.HandleFlip(runHandler.GetHorizontal(), transform);
        }

        // Dash yönü ayarý:
        dashHandler.SetFacingDirection(flipHandler.IsFacingRight);
        dashHandler.TryDash(runHandler.GetHorizontal(), Input.GetAxisRaw("Vertical"), () => jumpHandler.IsGrounded());

        runHandler.SetLastOnGroundTime(jumpHandler.GetLastOnGroundTime());  
        runHandler.UpdateHandler();

        jumpHandler.UpdateHandler();
        float lastOnGround = jumpHandler.GetLastOnGroundTime();

        wallHandler.UpdateHandler(jumpHandler.IsGrounded(), runHandler.GetHorizontal());



    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dashreset"))
        {
            dashHandler.ResetDash();
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            deathHandler.TriggerDeath();
        }
    }
    void FixedUpdate()
    {
        if (!wallHandler.IsWallJumping())
        {
            runHandler.FixedUpdateHandler();
        }
        runHandler.FixedUpdateHandler();
    }
}
