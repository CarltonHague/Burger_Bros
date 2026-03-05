using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Player_Dash dashScript;
    private int playerFacingDirection = 1;

    public int getPlayerFacingDirection => playerFacingDirection;
    public Vector2 getMoveInput => moveInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dashScript = GetComponent<Player_Dash>();
       

    }

    // Update is called once per frame
    void Update()
    {


    }
    private void FixedUpdate()
    {
        if(dashScript.getIsDashing)
        {
            return;
        }
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

    }


    public void move(InputAction.CallbackContext context)
    {
        if (dashScript.getIsDashing)
        {
            return;
        }
            moveInput = context.ReadValue<Vector2>();

        if(moveInput.x != 0)
        {
            playerFacingDirection = (int)Mathf.Sign(moveInput.x);
        }
        
    }

}
    