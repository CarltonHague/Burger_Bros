using UnityEngine;

public class Switch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isPressed = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField]private Sprite unpressedSprite;
    [SerializeField] private Sprite pressedSprite;
    public bool getIsPressed => isPressed;
    public void triggerSwitch(bool value) => isPressed = value;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unpressedSprite;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            spriteRenderer.sprite = pressedSprite;
        }
        else
        {
            spriteRenderer.sprite = unpressedSprite;
        }
    }
    

}
