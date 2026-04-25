using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 0.5f;

    private Transform cam;
    private Vector3 startPos;
    private Vector3 camStartPos;

    void Start()
    {
        cam = Camera.main.transform;
        startPos = transform.position;
        camStartPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 camDelta = cam.position - camStartPos;

        transform.position = startPos + new Vector3(
            camDelta.x * parallaxFactor,
            0,
            0
        );
    }
}