using UnityEngine;

public class FlipHandler : MonoBehaviour
{
    private bool isFacingRight = true;

    public bool IsFacingRight => isFacingRight;

    public void HandleFlip(float horizontalInput, Transform targetTransform)
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = targetTransform.localScale;
            localScale.x *= -1f;
            targetTransform.localScale = localScale;
        }
    }

    public void ForceSetDirection(bool facingRight, Transform targetTransform)
    {
        if (isFacingRight != facingRight)
        {
            HandleFlip(facingRight ? 1f : -1f, targetTransform);
        }
    }
}
