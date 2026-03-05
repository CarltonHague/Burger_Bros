using UnityEngine;
using UnityEngine.InputSystem;
public class Player_Jump : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 5f;
    private float gravity = -9.81f;
    private Rigidbody2D rb;
    private Vector2 moveInput;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // get the gravity value for calculating jump height
        gravity = Physics2D.gravity.y;
        gravity *= rb.gravityScale;

    }
    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Sqrt(jumpHeight * -2f * gravity));
        }
    }
    bool isGrounded()
    {
        return rb.IsTouchingLayers(LayerMask.GetMask("Floor"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
