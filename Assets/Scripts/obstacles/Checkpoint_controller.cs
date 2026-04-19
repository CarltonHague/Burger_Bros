using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint_controller : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider2D;
    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Set the checkpoint as active
            animator.SetTrigger("Reached flag");
            // Disable the collider to prevent multiple triggers
            boxCollider2D.enabled = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
