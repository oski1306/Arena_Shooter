using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Shooting : MonoBehaviour
{
    private InputAction shootInput;
    private InputAction powerUp1;

    private Animator gunAnimator;

    [SerializeField] private float range = 100f;
    [SerializeField] private GameObject shootEffectSprite;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Material hitMaterial;


    void Start()
    {
        gunAnimator = gameObject.GetComponent<Animator>();

        shootInput = InputSystem.actions.FindAction("Attack");
        powerUp1 = InputSystem.actions.FindAction("PowerUp1");

        GameManager.canShoot = true;
    }

    void Update()
    {
        Shoot();

        if (powerUp1.WasPressedThisFrame())
        {
            Debug.Log("1 is pressed!");
        }
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
                MeshRenderer enemyMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                Material enemyMaterial = enemyMeshRenderer.material;

                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    enemyMeshRenderer.material = hitMaterial;
                    enemy.health = enemy.health - 20;
                    enemy.ChangeMaterial();
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
