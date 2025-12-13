using UnityEngine;

public class PowerShards : MonoBehaviour
{
    [SerializeField] private GameObject powerShard;
    [SerializeField] private BossManager bossManager;

    private float _rndX;
    private float _rndY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossManager.CurrentBoss.beenHit)
        {
            //Left side 
            _rndX = Random.Range(-18, -9);
            _rndY = Random.Range(-7, 8);
            Instantiate(powerShard, new Vector3(_rndX, _rndY, 0), Quaternion.identity);
            
            //Right side 
            _rndX = Random.Range(8, 19);
            _rndY = Random.Range(-7, 8);
            Instantiate(powerShard, new Vector3(_rndX, _rndY, 0), Quaternion.identity);
            
            //Middle
            _rndX = Random.Range(-8, 9);
            _rndY = Random.Range(-7, 6);
            if (_rndY >= -1)
            {
                _rndY += 2;
            }
            GameObject shard = Instantiate(powerShard, new Vector3(_rndX, _rndY, 0), Quaternion.identity);
            shard.transform.parent = transform;

            bossManager.CurrentBoss.beenHit = false;
        }
    }

}
