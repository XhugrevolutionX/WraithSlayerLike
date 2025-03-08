using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour 
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float delay = 2f;

    private Boss _boss;

    private Coroutine _respawnDelay;
    private bool _canRespawn = false;
    private bool _isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boss = GetComponentInChildren<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_boss != null)
        {
            _isDead = false;
        }
        else
        {
            if (_isDead == false)
            {
                if (_respawnDelay != null)
                {
                    StopCoroutine(_respawnDelay);
                }

                StartCoroutine("RespawnDelay");
            }
           
        }

        if (_canRespawn)
        {
            Instantiate(bossPrefab, transform.position, Quaternion.identity, transform);
            _boss = GetComponentInChildren<Boss>();
            _canRespawn = false;
            _isDead = false;
        }
    }

    IEnumerator RespawnDelay()
    {
        _isDead = true;
        yield return new WaitForSeconds(delay);
        _canRespawn = true;
    }
}