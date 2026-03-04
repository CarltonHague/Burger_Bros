using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    private float gravity = -9.81f;
    Rigidbody2D rb;
    Vector2 moveInput;
    
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // get the gravity value for calculating jump height
        gravity = Physics2D.gravity.y;
        gravity *= rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }


    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    bool isGrounded()
    {
        return rb.IsTouchingLayers(LayerMask.GetMask("Platform"));
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Sqrt(jumpHeight * -2f * gravity));
        }
    }
}
