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
            reloadGun();
        }
    }
}
