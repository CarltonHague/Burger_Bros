using UnityEngine;

public class CloudLayer : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 0.2f;
    [SerializeField] private Vector2 windSpeed = new Vector2(0.05f, 0f);

    private Transform cam;
    private Vector3 lastCamPos;
    private Vector3 startPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
        startPos = transform.position;
    }

    void LateUpdate()
    {
        // camera parallax
        Vector3 camDelta = cam.position - lastCamPos;

        // wind movement (always moving)
        startPos += new Vector3(
            windSpeed.x * Time.deltaTime,
            windSpeed.y * Time.deltaTime,
            0
        );

        transform.position = startPos + new Vector3(
            camDelta.x * parallaxFactor,
            camDelta.y * parallaxFactor,
            0
        );

        lastCamPos = cam.position;
    }
}