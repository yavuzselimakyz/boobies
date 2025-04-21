using System.Collections;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TriggerDeath()
    {
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
        }

        float alpha = sprite.color.a;
        Color color = sprite.color;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            sprite.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
