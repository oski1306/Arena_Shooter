using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private InputAction shootInput;
    private Animator gunAnimator;

    void Start()
    {
        gunAnimator = gameObject.GetComponent<Animator>();
        shootInput = InputSystem.actions.FindAction("Attack");
        GameManager.canShoot = true;
    }

    void Update()
    {
        Shoot();
        Debug.Log(GameManager.canShoot);
    }

    void Shoot()
    {    

        if (GameManager.canShoot == false)
            return;

        if (shootInput.WasPressedThisFrame())
        {
            gunAnimator.SetBool("IsShooting", true);
            GameManager.canShoot = false;
        }     
    }

    void Pump()
    {
        gunAnimator.SetBool("IsPumping", true);
        gunAnimator.SetBool("IsShooting", false);
    }

    void StopPump()
    {
        gunAnimator.SetBool("IsPumping", false);

        GameManager.canShoot = true;
    }
}
