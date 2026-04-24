using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    [SerializeField] Sprite noGrappleSprite;
    [SerializeField] Sprite grappleSprite;
    

    private SpriteRenderer spriteRenderer;
    
    public bool inRange = false;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetGrapplePointInRange(bool inRange)
    {
        this.inRange = inRange;
        if (inRange)
        {
            spriteRenderer.sprite = grappleSprite;
            
        }
        else
        {
            spriteRenderer.sprite = noGrappleSprite;
            
        }
    }
}
