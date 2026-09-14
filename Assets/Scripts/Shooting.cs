using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Shooting : MonoBehaviour
{
    private InputAction shootInput;
    private Animator gunAnimator;
    [SerializeField] private float range = 100f;
    [SerializeField] private GameObject shootEffectSprite;
    [SerializeField] private GameObject crosshair;

    void Start()
    {
        gunAnimator = gameObject.GetComponent<Animator>();
        shootInput = InputSystem.actions.FindAction("Attack");
        GameManager.canShoot = true;
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {    

        if (GameManager.canShoot == false)
            return;

        if (shootInput.WasPressedThisFrame())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            

            if (Physics.Raycast(ray, out hit, range))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    enemy.health = enemy.health - 20;
                }
            }

            gunAnimator.SetBool("IsShooting", true);
            GameManager.canShoot = false;
        }     
    }

    void Pump()
    {
        gunAnimator.SetBool("IsPumping", true);
        gunAnimator.SetBool("IsShooting", false);
        StartCoroutine(CrosshairCooldown());
    }

    void StopPump()
    {
        gunAnimator.SetBool("IsPumping", false);

        GameManager.canShoot = true;
    }

    void EnableShootEffect()
    {
        shootEffectSprite.SetActive(false);
        crosshair.SetActive(false);
    }

    void DisableShootEffect()
    {
        shootEffectSprite.SetActive(true);
        
    }

    IEnumerator CrosshairCooldown()
    {
        yield return new WaitForSeconds(0.1f * Time.deltaTime);
        crosshair.SetActive(true);
    }
}
