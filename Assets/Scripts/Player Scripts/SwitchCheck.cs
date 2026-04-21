using UnityEngine;

public class SwitchCheck : MonoBehaviour
{
    BoxCollider2D playerHitbox;
    private bool activated = false;
    //sound
    public AudioClip SFX;
    public float volume = 0.9f;

    private void Awake()
    {
            playerHitbox = GetComponent<BoxCollider2D>();
    }


    void playSound()
    {
        AudioSource.PlayClipAtPoint(SFX, transform.position, volume);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
            if(other.CompareTag("Switch"))
            {
                Switch switchScript = other.GetComponent<Switch>();
                if (switchScript != null)
                {
                    switchScript.triggerSwitch(true);
                    if (!activated)
                    {
                    activated = true;
                    playSound();
                    }
                }
        }
    }
}
