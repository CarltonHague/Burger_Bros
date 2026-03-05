using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float wallSlideSpeed = -2f;


    private Rigidbody2D rb;
    private Vector2 moveInput;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // get the gravity value for calculating jump height

    }

    // Update is called once per frame
    void Update()
    {


    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

    }


    public void move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

}
    