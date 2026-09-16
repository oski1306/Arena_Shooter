using UnityEngine;

public class ExplosionPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.explosionBullets = GameManager.explosionBullets + 1;
            Destroy(gameObject);
        }
    }
}
