using UnityEngine;

public class Break_Platform_Check : MonoBehaviour
{
    private BoxCollider2D boxCollider;


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        
            BreakPlatform platform = collision.gameObject.GetComponentInParent<BreakPlatform>();
            if (platform != null)
            {
                platform.setPlayerOnPlatform(true);
            }
        
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {

        BreakPlatform platform = collision.gameObject.GetComponentInParent<BreakPlatform>();
        if (platform != null)
        {
            platform.setPlayerOnPlatform(false);
        }
    }
}   
