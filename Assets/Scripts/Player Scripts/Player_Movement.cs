using UnityEngine;
using UnityEngine.InputSystem;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float airMoveSpeed = 2f;
    [SerializeField] private float maxAirMoveSpeed = 5f;
    [SerializeField] private float slipperyControl = 0.1f;
    [SerializeField] private float MaxSpeed = 10f;
    [SerializeField] private float directionChangeControl = 0.05f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Player_Jump Player_Jump;
    private bool isOnSlipperySurface = false;
    private int playerFacingDirection = 1;


    public bool canMove = true;
    public int getPlayerFacingDirection => playerFacingDirection;
    public Vector2 getMoveInput => moveInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player_Jump = GetComponent<Player_Jump>();


    }

    // Update is called once per frame
    void Update()
    {


    }
    private void FixedUpdate()
    {
        // if the player is not allowed to move, don't process movement input
        if (!canMove)
        {
            return;
        }
        // if the player is on the ground allow them to do left right movement at full speed
        if (!isOnSlipperySurface)
        {
            float targetSpeed = moveInput.x * speed;
            targetSpeed = Mathf.Clamp(targetSpeed, -MaxSpeed, MaxSpeed);
            rb.linearVelocityX = targetSpeed;



        }
        if (!Player_Jump.isInAir() && isOnSlipperySurface)
        {
            // if the player is on a surface and is not doing input let them slide 
            if (moveInput.x == 0)
            {

                return;
            }


            float control = 0;
            // if the player is trying to change direction on a slippery surface reduce control to make it harder to change direction
            if (Mathf.Sign(moveInput.x) != Mathf.Sign(rb.linearVelocityX))
            {
                control = slipperyControl * directionChangeControl;
            }
            else
            {
                control = slipperyControl;
            }
            // calculate the target speed based on player input and apply it to the player's velocity using Lerp for smooth acceleration and deceleration
            float targetSpeed = moveInput.x * speed;
            float newSpeed =  Mathf.Lerp(rb.linearVelocityX, targetSpeed, control);
            newSpeed = Mathf.Clamp(newSpeed, -MaxSpeed, MaxSpeed);

            // apply the new speed to the player's velocity while maintaining the current vertical velocity
            rb.linearVelocityX = newSpeed;
           


        }
        // if the player is in the air allow them to move but with less control and a max speed
        //if (Player_Jump.isInAir())
        //{
        //    float newAirMoveSpeed = rb.linearVelocityX + (moveInput.x * airMoveSpeed) * Time.fixedDeltaTime;
        //    newAirMoveSpeed = Mathf.Clamp(newAirMoveSpeed, -maxAirMoveSpeed, maxAirMoveSpeed);
        //    rb.linearVelocityX = newAirMoveSpeed;
        //}
    }


    public void move(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();

        if (moveInput.x != 0)
        {
            playerFacingDirection = (int)Mathf.Sign(moveInput.x);
        }

    }
    // when the player collides with a slippery surface set the bool to true 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slippery"))
        {
            isOnSlipperySurface = true;
        }
    }
    // when the player exits collision with a slippery surface set the bool to false
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slippery"))
        {
            isOnSlipperySurface = false;
        }

    }
}
    