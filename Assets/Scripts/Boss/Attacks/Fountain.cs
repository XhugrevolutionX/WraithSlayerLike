using System.Collections;
using UnityEngine;

public class Fountain : BossAttack
{
    [Header("Settings")]
    [SerializeField] private GameObject residuePrefab;
    [SerializeField] private int count = 20;
    [SerializeField] private float interval = 0.1f;
    
    public override void Fire(Transform boss, Transform target)
    {
        StartCoroutine(SpawnFountain(boss));
    }
    
    private IEnumerator SpawnFountain(Transform boss)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(residuePrefab, boss.position, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
        Destroy(gameObject);
    }
}
