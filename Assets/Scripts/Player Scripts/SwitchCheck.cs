using UnityEngine;

public class SwitchCheck : MonoBehaviour
{
    BoxCollider2D playerHitbox;
    private void Awake()
    {
            playerHitbox = GetComponent<BoxCollider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
            if(other.CompareTag("Switch"))
            {
                Switch switchScript = other.GetComponent<Switch>();
                if (switchScript != null)
                {
                    switchScript.triggerSwitch(true);
                }
        }
    }
}
