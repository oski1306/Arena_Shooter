using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text explosiveCount;
    void Update()
    {
        explosiveCount.text = GameManager.explosionBullets.ToString();
    }
}
