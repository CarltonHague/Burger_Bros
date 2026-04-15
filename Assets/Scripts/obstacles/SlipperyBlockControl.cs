using UnityEngine;

public class SlipperyBlockControl : MonoBehaviour
{
    [SerializeField] private float slipperyControl = 0.1f;
    private BoxCollider2D boxCollider;
    private PhysicsMaterial2D slipperyMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        slipperyMaterial = boxCollider.GetComponentInChildren<PhysicsMaterial2D>();
        slipperyMaterial.friction = slipperyControl;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
