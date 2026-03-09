using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Player_Shoot : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform LaunchOffset;
    [SerializeField] bool hasGun = true;
    [SerializeField] int maxBullets = 10;
    [SerializeField] float reloadSpeed = 2f;
    private int amountOfBullets = 10;
    public float shootCooldown = 0.5f;
    private bool onCooldown = false;
    //ammo ui
    public Image ammoUIDisplay;
    public Sprite[] ammoSpriteArray;



    private void reloadGun()
    {
        if (hasGun && amountOfBullets < maxBullets)
        {
            amountOfBullets += 1;
            changeAmmoSprite();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(reloadGun), reloadSpeed, reloadSpeed);
    }

    public void shoot(InputAction.CallbackContext context)
    {
        if (hasGun && amountOfBullets > 0 && !onCooldown)
        {
            amountOfBullets -= 1;
            Instantiate(projectilePrefab, LaunchOffset.position, transform.rotation);
            changeAmmoSprite();
            onCooldown = true;
            Invoke(nameof(turnOffCooldown), shootCooldown);
        }
    }
    private void turnOffCooldown()
    {
        onCooldown = false;
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void changeAmmoSprite()
    {
        ammoUIDisplay.sprite = ammoSpriteArray[amountOfBullets];
    }

}
