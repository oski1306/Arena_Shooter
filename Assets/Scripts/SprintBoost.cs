using UnityEngine;
using System.Collections;

public class SprintBoost : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.sprintBoost = true;
            StartCoroutine(SpeedBoostCountdown());
        }
    }

    IEnumerator SpeedBoostCountdown()
    {
        yield return new WaitForSeconds(15f);
        GameManager.sprintBoost = false;
    }
}
