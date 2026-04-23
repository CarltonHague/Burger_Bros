using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool animated = false;
    public Sprite[] animationFrames;
    private int frame = 0;
    public float animationSpeed;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (animated)
        {
            InvokeRepeating(nameof(animate), animationSpeed, animationSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void animate()
    {
        spriteRenderer.sprite = animationFrames[frame];
        frame++;
        if (frame >= animationFrames.Length)
        {
            frame = 0;
        }
    }

}
