using System.Collections;
using UnityEngine;

public class BoulderSpawner : BossAttack
{
    [Header("Settings")]
    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private int boulderCount = 10;
    [SerializeField] private float spawnInterval = 0.2f;
    
    public override void Fire(Transform boss, Transform target)
    {
        StartCoroutine(SpawnBoulders(target));
    }
    private IEnumerator SpawnBoulders(Transform target)
    {
        for (int i = 0; i < boulderCount; i++)
        {
            float randomX = Random.Range(-2f, 3f);
            Vector2 spawnPos = new Vector2(target.position.x + randomX, 7);
            
            Instantiate(boulderPrefab, spawnPos, Quaternion.identity);
            
            yield return new WaitForSeconds(spawnInterval);
        }
        // Destroy the spawner object when done
        Destroy(gameObject); 
    }
}
