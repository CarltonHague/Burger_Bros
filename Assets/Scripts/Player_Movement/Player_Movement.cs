using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float wallSlideSpeed = -2f;

    private float gravity = -9.81f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    bool isSlidingOnWall = false;


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
        isSlidingOnWall = isTouchingWall() && !isGrounded() && rb.linearVelocityY > 0;
        if (isSlidingOnWall)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocityY + gravity + wallSlideSpeed)* Time.deltaTime;
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        }
    }


    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    bool isGrounded()
    {
        return rb.IsTouchingLayers(LayerMask.GetMask("Floor"));
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Sqrt(jumpHeight * -2f * gravity));
        }
    }
    bool isTouchingWall()
    {
        bool leftWall = Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance, LayerMask.GetMask("Wall"));
        bool rightWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, LayerMask.GetMask("Wall"));
        return leftWall || rightWall;
    }
}
