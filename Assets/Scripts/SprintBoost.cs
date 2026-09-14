using UnityEngine;
using System.Collections;

public class SprintBoost : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.sprintBoost = true;
            StartCoroutine(SpeedBoostCountdown());

            boxCollider.enabled = false;
            meshRenderer.enabled = false;
        }
    }

    IEnumerator SpeedBoostCountdown()
    {
        yield return new WaitForSeconds(15f);
        GameManager.sprintBoost = false;
        Destroy(gameObject);
    }
}
