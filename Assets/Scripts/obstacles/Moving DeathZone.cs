using UnityEngine;

public class MovingDeathZone : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float resetOffset = 5;
    [SerializeField] private float offscreenSpeed = 10f;
    private Camera mainCamera;
    private float heightOfShape;
    private void Awake()
    {
        // get the height of the death zone's shape
        heightOfShape = GetComponent<Collider2D>().bounds.size.y;
        mainCamera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void resetToCheckpoint(Transform checkpoint)
    {  
        // set the death zone's position to the bottom of the camera 
        transform.position = new Vector3(transform.position.x, checkpoint.position.y - (heightOfShape/ 2f) - resetOffset, transform.position.z);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move the death zone upwards using the moveSpeed variable
        if (isOnScreen())
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            
        }
        else
        {
            transform.position += Vector3.up * offscreenSpeed * Time.deltaTime;
        }
    }
    private bool isOnScreen()
    {
        // check if the death zone is above the top of the camera's view
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        return viewportPos.y >= 0f && viewportPos.y <= 1f;
    }
}
