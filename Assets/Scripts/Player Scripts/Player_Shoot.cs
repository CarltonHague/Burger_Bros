using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Player_Shoot : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform LaunchOffset;
    [SerializeField] bool hasGun = true;
    [SerializeField] int maxBullets = 5;
    //[SerializeField] float reloadSpeed = 2f;
    [SerializeField] int amountOfBullets = 5;
    public float shootCooldown = 0.5f;
    private bool onCooldown = false;
    //ammo ui
    public Image ammoUIDisplay;
    public Sprite[] ammoSpriteArray;
    //rotation
    public GameObject rotationPoint;
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector3 rotation;
    private float rotZ = 0f;

    //sound
    public AudioClip SFX;
    public float volume = 0.9f;

    void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rotation = mousePos - rotationPoint.transform.position;
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotationPoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void playSound()
    {
        AudioSource.PlayClipAtPoint(SFX, transform.position, volume);
    }

    private void reloadGun()
    {
        if (hasGun && amountOfBullets < maxBullets)
        {
            amountOfBullets  = maxBullets;
            changeAmmoSprite();
        }
    }


    public void shoot(InputAction.CallbackContext context)
    {
        if (hasGun && amountOfBullets > 0 && !onCooldown)
        {
            amountOfBullets -= 1;
            Instantiate(projectilePrefab, LaunchOffset.position, LaunchOffset.transform.rotation);
            changeAmmoSprite();
            playSound();
            onCooldown = true;
            Invoke(nameof(turnOffCooldown), shootCooldown);
        }
    }
    private void turnOffCooldown()
    {
        onCooldown = false;
    }

    private void changeAmmoSprite()
    {
        ammoUIDisplay.sprite = ammoSpriteArray[amountOfBullets];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ketchup-PickUp")
        {
            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance < 2f)
            {
                reloadGun();
            }
        }
    }
    public void resetAmmo()
    {
        amountOfBullets = maxBullets;
        changeAmmoSprite();
    }
}
