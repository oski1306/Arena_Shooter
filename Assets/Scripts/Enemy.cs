using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    private Material startMaterial;
    private MeshRenderer enemyRenderer;

    void Start()
    {
        enemyRenderer = gameObject.GetComponent<MeshRenderer>();
        startMaterial = enemyRenderer.material;
    }
    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

   public void ChangeMaterial()
    {
        StartCoroutine(MaterialChange());
    }

    IEnumerator MaterialChange()
    {
        yield return new WaitForSeconds(.15f);
        enemyRenderer.material = startMaterial;
    }
}
