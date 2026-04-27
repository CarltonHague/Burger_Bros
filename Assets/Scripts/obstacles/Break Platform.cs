using UnityEngine;
using System.Collections;

public class BreakPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // swap these to sprites when the art is done for the platform
    Color originalColor, halfbroke = Color.orange, nearlyBroke = Color.red;
    [SerializeField] private float timeToBreakInSeconds = 3f;
    [SerializeField] private float timeToRespawnInSeconds = 5f;
    [SerializeField] Sprite orginalSprite, HalfBroke, NearlyBroke;
    bool playerOnPlatform = false;
    public void setPlayerOnPlatform(bool value) => playerOnPlatform = value;
    bool isBreaking = false;
    private BoxCollider2D[] boxColliders;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxColliders = GetComponentsInChildren<BoxCollider2D>();
        originalColor = spriteRenderer.color;
        isBreaking = false;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerOnPlatform)
        {
            return;
        }
        else if(playerOnPlatform && !isBreaking)
        {
            isBreaking = true;
            StartCoroutine(breakPlatformCoroutine());
        }
    }


    private IEnumerator breakPlatformCoroutine()
    {
        if(isBreaking)
        {
            spriteRenderer.sprite = HalfBroke;
            yield return new WaitForSeconds(timeToBreakInSeconds / 2);
          
            spriteRenderer.sprite = NearlyBroke;
            yield return new WaitForSeconds(timeToBreakInSeconds / 2);

            spriteRenderer.enabled = false;
            foreach (BoxCollider2D boxCollider in boxColliders)
            {
                boxCollider.enabled = false;
            }
   
            yield return new WaitForSeconds(timeToRespawnInSeconds);
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = orginalSprite;
            foreach (BoxCollider2D boxCollider in boxColliders)
            {
                boxCollider.enabled = true;
            }
            isBreaking = false;
        }
    }
    public void ResetPlatform()
    {
       spriteRenderer.enabled = true;
       spriteRenderer.sprite = orginalSprite;
       

    }
}
