using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float doorOpenHeight = 3f;
    [SerializeField] private float doorOpeningSpeed = 1f;
    Rigidbody2D rb;
    [SerializeField] private Switch switchForThisDoor;
    enum DoorState
    {
        Open,
        Closed,
        opening,
    }
    DoorState currentDoorState = DoorState.Closed;
    private Vector2 closedPosition;
    private Vector2 openPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        //switchForThisDoor = GetComponentInParent<Transform>().GetComponentInChildren<Switch>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        closedPosition = rb.position;
        openPosition = closedPosition + Vector2.up * doorOpenHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if(switchForThisDoor.getIsPressed && currentDoorState == DoorState.Closed)
        {
            currentDoorState = DoorState.opening;

            
        }
        if(rb.position == openPosition)
        {
            currentDoorState = DoorState.Open;
        }

    }

    void FixedUpdate()
    {
        if(currentDoorState == DoorState.opening)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, openPosition, doorOpeningSpeed * Time.fixedDeltaTime));
        }
    }
}
