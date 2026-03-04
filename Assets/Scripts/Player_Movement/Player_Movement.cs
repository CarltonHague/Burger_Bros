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
    Vector2 velocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity.y;
        gravity *= rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        rb.linearVelocity = move * speed * Time.deltaTime;

        velocity.y += gravity * Time.deltaTime;
        rb.linearVelocityY += velocity.y * Time.deltaTime;
    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
