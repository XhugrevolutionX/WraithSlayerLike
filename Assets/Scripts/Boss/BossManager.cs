using System;
using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour 
{
    [SerializeField] private GameObject boss0Prefab;
    [SerializeField] private GameObject boss1Prefab;
    [SerializeField] private GameObject boss2Prefab;
    
    public static int SelectedBossIndex = 0;
    
    [SerializeField] private int bossToSpawn = 0;
    [SerializeField] private float delay = 2f;

    public Boss CurrentBoss { get; private set; }
    
    private Coroutine _respawnDelay;
    
    void Start()
    {
        bossToSpawn = SelectedBossIndex;
        
        SpawnBoss();
    }

    private void StartRespawnDelay()
    {
        if (_respawnDelay != null)
        {
            StopCoroutine(_respawnDelay);
        }

        StartCoroutine(nameof(RespawnDelay));
    }

    private void SpawnBoss()
    {
        // bossToSpawn += 1;
        // if (bossToSpawn > 2)
        //     bossToSpawn = 0;
        
        GameObject boss;
        switch (bossToSpawn)
        {
            case 0:
                boss = Instantiate(boss0Prefab, transform.position, Quaternion.identity);
                break;
            case 1:
                boss = Instantiate(boss1Prefab, transform.position, Quaternion.identity);
                break;
            case 2:
                boss = Instantiate(boss2Prefab, transform.position, Quaternion.identity);
                break;
            default:
                boss = null;
                break;
        }
        boss.transform.parent = transform;
        CurrentBoss = boss.GetComponent<Boss>();

        CurrentBoss.deathAction += StartRespawnDelay;
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(delay);
        SpawnBoss();
    }
}